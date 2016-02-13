using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private int health;
	private float b = 0.9f;
	private float increment = 0.005f;
	private BrickController brickController;
	private BallController ballController;

	void Start () {
		brickController = GameObject.FindObjectOfType<BrickController> ();
		ballController = GameObject.FindObjectOfType<BallController> ();
		spriteRenderer = gameObject.GetComponentsInChildren<SpriteRenderer> ()[0];
		health = ballController.getBallCount ();
	}

	void Update () {
		b += increment;
		// spriteRenderer.color = new Color (b - 0.2f, b - 0.4f, b, 0.7f);
		if (b >= 0.95f || b <= 0.7f) {
			increment *= -1;
		}
		Color brickColor = getBrickColor (health);
		spriteRenderer.color = brickColor;
	}
	public Color getBrickColor(int health) {
		return brickController.getColor ((float)health / (float)ballController.getBallCount());
	}
}
