using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrickImage : MonoBehaviour {
	private Image image;
	private float b = 0.9f;
	private float increment = 0.005f;

	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		b += increment;
		image.color = new Color (b - 0.2f, b - 0.4f, b, 0.7f);
		if (b >= 0.95f || b <= 0.7f) {
			increment *= -1;
		}
		Debug.Log (b);
	}
}
