using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private int health;
	private BrickController brickController;
	private BallController ballController;

	void Start () {
		brickController = GameObject.FindObjectOfType<BrickController> ();
		ballController = GameObject.FindObjectOfType<BallController> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		health = ballController.getBallCount ();
		MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
		meshRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
		meshRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		textMesh.text = health.ToString();
	}

	void Update () {
		Color brickColor = getBrickColor (health);
		spriteRenderer.color = brickColor;
	}
	public Color getBrickColor(int health) {
		return brickController.getColor ((float)health / (float)ballController.getBallCount());
	}
}
