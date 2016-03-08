using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoardManager : MonoBehaviour {

	public Text bsetScoreObject;
	public Text scoreObject;

	private int bestScore = 0;
	private int score = 0;

	// Use this for initialization
	void Start () {
		// TODO init bsetScore
		updateScore ();
	}
	
	// Update is called once per frame
	void Update () {
		updateScore ();
	}

	private void updateScore() {
		if (scoreObject != null) {
			scoreObject.text = score + "";
		}
		if (bsetScoreObject != null) {
			bsetScoreObject.text = bestScore + "";
		}
	}

	public void IncreaseScore() {
		score++;
		if (bestScore < score)
		{
			bestScore = score;
		}
	}
}
