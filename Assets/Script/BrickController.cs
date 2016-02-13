using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {
	private float horizonSize = 0.96f;
	private float verticalSize = 0.6f;
	private float[] horizonPositions = {-2.4f, -1.44f, -0.48f, 0.48f, 1.44f, 2.4f};
	private float[] verticalPositions = {-2.4f, -1.8f, -1.2f, -0.6f, 0.0f, 0.6f, 1.2f, 1.8f, 2.4f};
	private Color maxColor;
	private Color minColor;

	// Use this for initialization
	void Start () {
		maxColor = (Instantiate(Resources.Load("Prefab/MaxColor" ,typeof(GameObject))) as GameObject).GetComponent<SpriteRenderer>().color;
		minColor = (Instantiate(Resources.Load("Prefab/MinColor" ,typeof(GameObject))) as GameObject).GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Color getColor(float rate) {
		float r = applyRate (minColor.r, maxColor.r, rate);
		float g = applyRate (minColor.g, maxColor.g, rate);
		float b = applyRate (minColor.b, maxColor.b, rate);
		float a = applyRate (minColor.a, maxColor.a, rate);
		return new Color (r, g, b, a);
	}
	public float applyRate(float min, float max, float rate) {
		float diff = max - min;
		return diff * rate + min;
	}
	public void drawNewBrick() {
		// FIXME
		GameObject brick = Instantiate(Resources.Load("Prefab/Brick" ,typeof(GameObject))) as GameObject;
		brick.transform.position = new Vector3(-2.4f, 2.4f, 0.0f);
	}
}
