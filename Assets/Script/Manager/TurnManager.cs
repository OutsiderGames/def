using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {
	private bool gameOver = false;
	private int turn = 0;
	private int maxBrick = 2;
	private float xSize = 0.96f;
	private float ySize = 0.6f;
	private float[] xPositions = {-2.4f, -1.44f, -0.48f, 0.48f, 1.44f, 2.4f};
	private float yPosition = 2.4f;
	private float[] yPositions = {-2.4f, -1.8f, -1.2f, -0.6f, 0.0f, 0.6f, 1.2f, 1.8f, 2.4f};
	private float yTranslate = -0.6f;
	private float yDeadLine = -2.4f;
	private BrickManager brickManager;
	private ScoreBoardManager scoreBoardManager;
	private GameObject gameOverPopup;
	private GameObject startPopup;

	private GameObject[] clearBrickObjects = {};

	private BonusBall bonusBallPrefab;

	// Use this for initialization
	void Start () {
		brickManager = GameObject.FindObjectOfType<BrickManager> ();
		scoreBoardManager = GameObject.FindObjectOfType<ScoreBoardManager> ();
		GameObject[] popupObjects = GameObject.FindGameObjectsWithTag ("Popup");
		foreach (GameObject popupObject in popupObjects) {
			if (popupObject.name == "GameOverPopup") {
				gameOverPopup = popupObject;
				gameOverPopup.SetActive (false);
			} else if (popupObject.name == "StartPopup") {
				startPopup = popupObject;
			}
		}

		bonusBallPrefab = Resources.Load("Prefab/BonusBall", typeof(BonusBall)) as BonusBall; 
	}
	
	// Update is called once per frame
	void Update () {
		// FIXME delete test
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
	public void startGame() {
		increateTurn ();
		startPopup.SetActive (false);
		Debug.Log ("Game Start.");
	}
	public void increateTurn() {
		if (gameOver) {
			Debug.Log ("Already GameOver.");
			return;
		};

		int[] xPositions = { 0, 1, 2, 3, 4, 5 };
		List<int> tmpXPositionCandidate = new List<int> (xPositions);
		for (int i = 0; i < tmpXPositionCandidate.Count; i++) {
			int temp = tmpXPositionCandidate[i];
			int randomIndex = Random.Range(i, tmpXPositionCandidate.Count);
			tmpXPositionCandidate[i] = tmpXPositionCandidate[randomIndex];
			tmpXPositionCandidate[randomIndex] = temp;
		}
		LinkedList<int> xPositionCandidate = new LinkedList<int> (tmpXPositionCandidate);

		turn++;
		scoreBoardManager.IncreaseScore ();
		drawNewBricks (xPositionCandidate);
		moveDownBricks ();
		drawNewBonusBall (xPositionCandidate);
		moveDownBonusBalls ();
	}
	void drawNewBricks(LinkedList<int> xPositionCandidate) {
		int brickCount = getBrickCount ();
		for (int i = 0; i < brickCount; i++) {
			int xIndex = xPositionCandidate.First.Value;
			brickManager.drawNewBrick (new Vector3(xPositions[xIndex], yPosition, 0.0f));
			xPositionCandidate.RemoveFirst ();
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
			if (brickObject.transform.position.y <= yDeadLine) {
				gameOver = true;
			}
		}
		if (gameOver) {
			gameOverPopup.SetActive (true);
		}
	}
	void drawNewBonusBall(LinkedList<int> xPositionCandidate) {
		int xIndex = xPositionCandidate.First.Value;
		Instantiate (bonusBallPrefab, new Vector3 (xPositions [xIndex], yPosition, 0.0f), Quaternion.identity);
	}
	void moveDownBonusBalls() {
		GameObject[] bonusBalls = GameObject.FindGameObjectsWithTag ("BonusBall");
		foreach (GameObject bonusBall in bonusBalls) {
			bonusBall.transform.Translate (new Vector3 (0.0f, yTranslate, 0.0f));
			if (bonusBall.transform.position.y <= yDeadLine) {
				// TODO ?
			}
		}
	}

	public void testClearBrick() {
		if (gameOver) {
			return;
		}
		clearBrickObjects = GameObject.FindGameObjectsWithTag ("Brick");
	}

	public void restart() {
		gameOver = false;
		turn = 0;
		maxBrick = 2;
	}
}
