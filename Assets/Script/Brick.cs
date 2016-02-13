using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private float b = 0.9f;
	private float increment = 0.005f;
	private float horizonSize = 0.96f;
	private float verticalSize = 0.6f;
	private float[] horizonPositions = {-2.4f, -1.44f, -0.48f, 0.48f, 1.44f, 2.4f};
	private float[] verticalPositions = {-2.4f, -1.8f, -1.2f, -0.6f, 0.0f, 0.6f, 1.2f, 1.8f, 2.4f};

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		b += increment;
		spriteRenderer.color = new Color (b - 0.2f, b - 0.4f, b, 0.7f);
		if (b >= 0.95f || b <= 0.7f) {
			increment *= -1;
		}
		Debug.Log (b);
	}
}
