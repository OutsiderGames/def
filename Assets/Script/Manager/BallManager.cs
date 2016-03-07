using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

	public int currentBallCount;

	private bool canFire = true;
	private int flyingBallCount = 0;

	public float ballStartPositionX = 0f;
	public float ballStartPositionY = -2.79f;
	public float ballFireIntervalSecond = 0.06f;
	public float ballSpeed = 8f;
	public int maxBallCount = 100;

	private Ball ballPrefab;
	
	private Ball startBall;
	private Ball nextBall;

	private Ball[] balls;
	private Ball firstFlooredBall;
	
	private TurnManager turnManager;

	void Start () {
		ballPrefab = Resources.Load("Prefab/Ball", typeof(Ball)) as Ball;

		//startBall = Instantiate(ballPrefab, new Vector3(ballStartPositionX, ballStartPositionY), Quaternion.identity) as Ball;
		//startBall.ballManager = this;

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
			//Debug.Log("X : " + toPosition.x + " Y : " + toPosition.y);
		}

		if (Input.GetMouseButtonUp(0)) {
			Vector3 toPosition = Input.mousePosition;
			toPosition.z = 1;

			toPosition = Camera.main.ScreenToWorldPoint(toPosition);
			toPosition.z = 0;

			StartCoroutine(FireBalls(toPosition));
		}
	}

	IEnumerator FireBalls(Vector3 to) {
		canFire = false;
		firstFlooredBall = null;

		Vector3 from = balls[0].transform.position;
		Vector3 direction = (to - from).normalized * ballSpeed;

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
		IncreaseBallCount(1); //Test Purpose

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
}
