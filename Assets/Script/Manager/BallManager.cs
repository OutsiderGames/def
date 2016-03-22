using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

	public int currentBallCount;

	private bool canFire = true;
	private int flyingBallCount = 0;
	private float maxFireDegreeCos;
	private Vector3 maxFireLeftDirection;
	private Vector3 maxFireRightDirection;

	public float ballStartPositionX = 0f;
	public float ballStartPositionY = -2.79f;
	public float ballFireIntervalSecond = 0.06f;
	public float ballSpeed = 8f;
	public int maxBallCount = 100;
	public float maxFireDegree = 80;

	private Ball ballPrefab;
	
	private Ball[] balls;
	private Ball firstFlooredBall;
	private Vector3 lastInputPosition;
	private Vector3 fireDirection;
	
	private TurnManager turnManager;

	private int getNumberOfBallsOnThisFire = 0; // onetime value
	
	void Start () {
		ballPrefab = Resources.Load("Prefab/Ball", typeof(Ball)) as Ball;

		maxFireDegreeCos = Mathf.Cos(maxFireDegree * Mathf.Deg2Rad);
		maxFireRightDirection = new Vector3(Mathf.Sin(Mathf.Deg2Rad * maxFireDegree), Mathf.Cos(Mathf.Deg2Rad * maxFireDegree), 0).normalized;
		maxFireLeftDirection = new Vector3(-maxFireRightDirection.x, maxFireRightDirection.y, 0).normalized;

		balls = new Ball[maxBallCount];
		int ballsToInstantiate = Mathf.Min(currentBallCount, maxBallCount);
		for (int i = 0; i < ballsToInstantiate; i++) {
			balls[i] = Instantiate(ballPrefab, new Vector3(ballStartPositionX, ballStartPositionY), Quaternion.identity) as Ball;
			balls[i].ballManager = this;
		}

		turnManager = GameObject.FindObjectOfType<TurnManager>();
	}
	
	void Update () {
		if (canFire == false) {
			return;
		}
		
		if (Input.GetMouseButton(0)) {
			Vector3 toPosition = Input.mousePosition;

			if (lastInputPosition == toPosition) {
				return;
			} else {
				lastInputPosition = toPosition;
			}

			Vector3 from = balls[0].transform.position;

			toPosition.z = 1;
			toPosition = Camera.main.ScreenToWorldPoint(toPosition);
			toPosition.z = 0;

			Vector3 direction = (toPosition - from).normalized;
			float dotProductValue = Vector3.Dot(direction, Vector3.up);

			Debug.DrawRay(from, direction * 4, Color.cyan);
			//Debug.Log(direction);
			if (dotProductValue < maxFireDegreeCos) {
				//Debug.Log("TO LOW!!! ");
				direction = direction.x > 0 ? maxFireRightDirection : maxFireLeftDirection;
				Debug.DrawRay(from, direction * 4, Color.green);
			}

			fireDirection = direction;
		}

		if (Input.GetMouseButtonUp(0)) {
			lastInputPosition = lastInputPosition.normalized;
			StartCoroutine(FireBalls(fireDirection));
		}
	}

	IEnumerator FireBalls(Vector3 direction) {
		canFire = false;
		firstFlooredBall = null;

		direction = direction * ballSpeed;
		Debug.Log("Direction : " + direction);
		foreach (Ball ball in balls) {
			if (ball == null) {
				break;
			}

			ball.damage = 1;
			ball.conflicted = false;
			Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();

			rigidbody.velocity = direction;

			flyingBallCount++;

			yield return new WaitForSeconds(ballFireIntervalSecond);
		}
		
		yield return StartCoroutine(WaitBallReceive());
		//Maybe all ball touched floor
		//But not move to first floored ball, wait more
		yield return new WaitForSeconds(0.4f);

		turnManager.increateTurn();
//		IncreaseBallCount(1); //Test Purpose
		_IncreaseBall();

		canFire = true;
	}

	IEnumerator WaitBallReceive() {
		while (flyingBallCount != 0) {
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void ReceiveBall(Ball ball) {
		//Debug.Log("Recieve " + ball);
		flyingBallCount--;

		// First Recieved Ball
		if (firstFlooredBall == null) {
			firstFlooredBall = ball;
		} else {
			ball.MoveToOtherBall(firstFlooredBall);
		}
	}

	public void IncreaseBallCount(int count) {
		int ballsToCreate = count;
		
		if (currentBallCount + count > maxBallCount) {
			ballsToCreate = maxBallCount - currentBallCount;
		}

		int ballCreated = 0;
		Vector3 position = balls[0].transform.position;
		for (int i = 0; i < maxBallCount; i++) {
			if (balls[i] != null) {
				continue;
			}

			balls[i] = Instantiate(ballPrefab, position, Quaternion.identity) as Ball;
			balls[i].ballManager = this;

			ballCreated++;
			if (ballCreated == ballsToCreate) {
				break;
			}
		}

		currentBallCount += count;
	}
		
	public void GetBonusBall() {
		getNumberOfBallsOnThisFire ++;
	}

	private void _IncreaseBall() {
		if (getNumberOfBallsOnThisFire > 0) {
			IncreaseBallCount (getNumberOfBallsOnThisFire);
		}

		getNumberOfBallsOnThisFire = 0;
	}
}
