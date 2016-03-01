using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
	private int turn = 0;
	private int maxBrick = 2;
	private float xSize = 0.96f;
	private float ySize = 0.6f;
	private float[] xPositions = {-2.4f, -1.44f, -0.48f, 0.48f, 1.44f, 2.4f};
	private float yPosition = 2.4f;
	private float[] yPositions = {-2.4f, -1.8f, -1.2f, -0.6f, 0.0f, 0.6f, 1.2f, 1.8f, 2.4f};
	private float yTranslate = -0.6f;
	private BrickManager brickManager;
	private ScoreBoardManager scoreBoardManager;

	private GameObject[] clearBrickObjects = {};

	// Use this for initialization
	void Start () {
		brickManager = GameObject.FindObjectOfType<BrickManager> ();
		scoreBoardManager = GameObject.FindObjectOfType<ScoreBoardManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject brickObject in clearBrickObjects) {
			Brick brick = brickObject.GetComponent<Brick> ();
			brick.decreaseHealth (1);
			if (brickObject.activeSelf == false) {
				testClearBrick ();
			}
			break;
		}
	}

	public int getTurn() {
		return turn;
	}
	public void increateTurn() {
		turn++;
		drawNewBricks ();
		moveDownBricks ();
		scoreBoardManager.IncreaseScore ();
	}
	void drawNewBricks() {
		int brickCount = getBrickCount ();
		HashSet<int> indexSet = new HashSet<int> ();
		for (int i = 0; i < brickCount; i++) {
			int xIndex = Random.Range (0, 6);
			if (indexSet.Contains(xIndex)) {
				continue;
			}
			indexSet.Add (xIndex);
			brickManager.drawNewBrick (new Vector3(xPositions[xIndex], yPosition, 0.0f));
		}
	}
	int getBrickCount() {
		maxBrick = Mathf.Max (1, turn / 10);
		return Mathf.Min(5, Random.Range (1, maxBrick + 1));
	}
	void moveDownBricks() {
		GameObject[] brickObjects = GameObject.FindGameObjectsWithTag ("Brick");
		foreach (GameObject brickObject in brickObjects) {
			brickObject.transform.Translate (new Vector3 (0.0f, yTranslate, 0.0f));
		}
	}

	public void testClearBrick() {
		clearBrickObjects = GameObject.FindGameObjectsWithTag ("Brick");
	}
}
