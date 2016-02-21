using UnityEngine;
using System.Collections;

public class BrickManager : MonoBehaviour {
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
	public void drawNewBrick(Vector3 position) {
		// FIXME
		GameObject brick = Instantiate(Resources.Load("Prefab/Brick" ,typeof(GameObject))) as GameObject;
		brick.transform.position = position;
	}
}
