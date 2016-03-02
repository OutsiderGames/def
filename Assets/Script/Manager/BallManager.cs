using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

	public int currentBallCount;

	private int floorBallCount;
	private int flyingBallCount;

	private float ballStartPositionX;
	private float ballStartPositionY;
	private Ball ballPrefab;
	private float ballFireIntervalSecond;
	private float ballSpeed;

	private Ball startBall;
	private Ball nextBall;

	private TurnManager turnManager;

	void Start () {
		floorBallCount = currentBallCount;
		flyingBallCount = 0;
		ballStartPositionX = 0f;
		ballStartPositionY = -2.79f;
		ballFireIntervalSecond = 0.06f;
		ballPrefab = Resources.Load("Prefab/Ball", typeof(Ball)) as Ball;
		ballSpeed = 7f;

		startBall = Instantiate(ballPrefab, new Vector3(ballStartPositionX, ballStartPositionY), Quaternion.identity) as Ball;
		startBall.ballManager = this;

		turnManager = GameObject.FindObjectOfType<TurnManager> ();
		turnManager.increateTurn ();
	}
	
	void Update () {
		if (floorBallCount != currentBallCount) {
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
		Vector3 from = startBall.transform.position;
		Vector3 direction = (to - from).normalized * ballSpeed;

		for (int i = currentBallCount - 1; i >= 0; i--) {
			Ball ball = (i == 0) ? startBall : Instantiate(startBall) as Ball;
			Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();

			rigidbody.velocity = direction;

			ball.ballManager = this;

			floorBallCount--;
			flyingBallCount++;

			yield return new WaitForSeconds(ballFireIntervalSecond);
		}
	}

	public void ReceiveBall(Ball ball) {
		//Debug.Log("Recieve " + ball);
		floorBallCount++;
		flyingBallCount--;

		// First Recieved Ball
		if (nextBall == null) {
			nextBall = ball;
		} else {
			ball.RemoveBall(nextBall);
		}

		// Last Received Ball
		if (floorBallCount == currentBallCount) {
			//Debug.Log("All Received");
			startBall = nextBall;
			startBall.conflicted = false;
			nextBall = null;


			turnManager.increateTurn ();

			//Test for ball increase
			floorBallCount++;
			currentBallCount++;
		}
	}
}
