using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	private float b = 0.9f;
	private float increment = 0.005f;

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
