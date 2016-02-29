using UnityEngine;using System.Collections;public class Ball : MonoBehaviour {    public BallManager ballManager {        get; set;    }

    public bool conflicted {
        get; set;
    }

    private Rigidbody2D rigidbody;    

    void Start () {        rigidbody = GetComponent<Rigidbody2D>();        conflicted = false;	}		void Update () {		}    void OnCollisionEnter2D(Collision2D collision) {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("VerticalWall") || gameObject.CompareTag("HorizontalWall") || gameObject.CompareTag("Brick")) {
			//PlaySound()
			conflicted = true;
        } else if (gameObject.CompareTag("Floor") && conflicted) {
			Debug.Log("Floor!");
			rigidbody.velocity = Vector2.zero;
			ballManager.ReceiveBall(this);
		}
    }		    void OnTriggerEnter2D(Collider2D other) {        Debug.Log("[Ball]OnTriggerEnter");        if (other.CompareTag("VerticalWall")) {
            conflicted = true;
            rigidbody.velocity = new Vector2(-rigidbody.velocity.x, rigidbody.velocity.y);        } else if (other.CompareTag("HorizontalWall")) {
            conflicted = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -rigidbody.velocity.y);
        } else if (other.CompareTag("Floor") && conflicted) {            rigidbody.velocity = Vector2.zero;            ballManager.ReceiveBall(this);        } else if (other.CompareTag("Brick")) {            Debug.Log("Brick!");                    }    }}