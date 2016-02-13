using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	private int maxCount = 1;
	private int currentCount = 1;
	private BrickController brickController;

	// Use this for initialization
	void Start () {
		brickController = GameObject.FindObjectOfType<BrickController> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int getBallCount() {
		return maxCount;
	}
	public void increateBallCount() {
		maxCount++;
		brickController.drawNewBrick ();
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brick in bricks) {
			brick.transform.Translate (new Vector3 (0.0f, -0.6f, 0.0f));
		}
	}
}
