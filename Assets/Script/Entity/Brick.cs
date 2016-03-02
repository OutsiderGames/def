using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private int health;
	private BrickManager brickManager;
	private TurnManager turnManager;

	void Start () {
		brickManager = GameObject.FindObjectOfType<BrickManager> ();
		turnManager = GameObject.FindObjectOfType<TurnManager> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		health = turnManager.getTurn ();
		MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
		meshRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
		meshRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
		refreshBrick ();
	}

	void Update () {
		// TODO remove refresh on update??
		refreshBrick ();
	}
	void refreshBrick() {
		Color brickColor = getBrickColor (health);
		spriteRenderer.color = brickColor;
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		textMesh.text = health.ToString();
	}
	public Color getBrickColor(int health) {
		return brickManager.getColor ((float)health / (float)turnManager.getTurn());
	}
	public void decreaseHealth(int damage) {
		health -= damage;
		if (health > 0) {
			refreshBrick ();
		} else {
			gameObject.SetActive (false);
		}
	}
}
