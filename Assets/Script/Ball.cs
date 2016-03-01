using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public BallManager ballManager {
		get; set;
	}

	public bool conflicted {
		get; set;
	}

	private Rigidbody2D rigidbody2d;

	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		conflicted = false;
	}

	void Update () {

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
	}

	void OnCollisionExit2D (Collision2D collision) {
		//Debug.Log("[Ball]onCollisionExit");
		// Prevent infinite horizontal moving
		if (rigidbody2d.velocity.y == 0f) {
			Debug.LogWarning("y Velocity is 0!!");
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -0.3f);
		}
	}

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
