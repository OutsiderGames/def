using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public BallManager ballManager {
		get; set;
	}

	public bool conflicted {
		get; set;
	}

	public int damage {
		get; set;
	}

	private Rigidbody2D rigidbody2d;
	public float ballMoveDurationSecond = 0.2f;

	private Vector2 lastCollisionVelocity;
	private Vector2 ballMoveStartPosition;
	private Vector2 ballMoveEndPosition;
	private float ballMoveStartTime = 0;

	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		conflicted = false;
		lastCollisionVelocity = Vector2.zero;
	}

	void Update () {
		if (ballMoveStartTime == 0) {
			return;
		}

		float ballMoveProgress = (Time.time - ballMoveStartTime) / ballMoveDurationSecond;
		ballMoveProgress = Mathf.Min(ballMoveProgress, 1f);
		transform.position = Vector2.Lerp(ballMoveStartPosition, ballMoveEndPosition, ballMoveProgress);

		if (ballMoveProgress == 1f) {
			ballMoveStartTime = 0;
		}
	}
	
	public void MoveToOtherBall (Ball firstBall) {
		ballMoveStartPosition = transform.position;
		ballMoveEndPosition = firstBall.transform.position;
		ballMoveStartTime = Time.time;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		//Debug.Log("[Ball]onCollisionEnter");
		GameObject gameObject = collision.gameObject;

		if (gameObject.CompareTag("VerticalWall")) {
			//PlaySound()
		} else if (gameObject.CompareTag("HorizontalWall") || gameObject.CompareTag("Brick")) {
			//PlaySound()
			conflicted = true;
		} else if (gameObject.CompareTag("Floor") && conflicted) {
			rigidbody2d.velocity = Vector2.zero;
			ballManager.ReceiveBall(this);
		}

		if (rigidbody2d != null) {
			lastCollisionVelocity = rigidbody2d.velocity;
		}
	}

	void OnCollisionExit2D (Collision2D collision) {
		//Debug.Log("[Ball]onCollisionExit");
		// Prevent infinite horizontal moving
		if (rigidbody2d.velocity.y == 0f) {
			float newVelocityY = lastCollisionVelocity.y;
			if (newVelocityY == 0) {
				newVelocityY = -0.8f;
			}
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, newVelocityY);
		}

		if (collision.gameObject.CompareTag("Brick")) {
			Brick brick = collision.gameObject.GetComponent<Brick>();
			brick.decreaseHealth(damage);
		}
	}

	/*
	 * Trigger 방식으로 공을 움직일 때 사용하는 코드.
	 * 지금은 쓸모 없는 상태
	 */
	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log("[Ball]OnTriggerEnter");
		bool setConflicted = true;

		if (other.CompareTag("VerticalWall")) {
			setConflicted = false;
			rigidbody2d.velocity = new Vector2(-rigidbody2d.velocity.x, rigidbody2d.velocity.y);
		} else if (other.CompareTag("HorizontalWall")) {
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -rigidbody2d.velocity.y);
		} else if (other.CompareTag("Floor") && conflicted) {
			rigidbody2d.velocity = Vector2.zero;
			ballManager.ReceiveBall(this);
		} else if (other.CompareTag("VerticalBrick")) {
			Debug.Log("VerticalBrick!");
			rigidbody2d.velocity = new Vector2(-rigidbody2d.velocity.x, rigidbody2d.velocity.y);
		} else if (other.CompareTag("HorizontalBrick")) {
			Debug.Log("HorizontalBrick!");
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -rigidbody2d.velocity.y);
		}

		if (setConflicted) {
			conflicted = true;
		}
	}
}
