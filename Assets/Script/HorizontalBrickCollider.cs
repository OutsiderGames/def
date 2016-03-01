using UnityEngine;
using System.Collections;

public class HorizontalBrickCollider : MonoBehaviour {

	void Start () {
		EdgeCollider2D top = gameObject.AddComponent<EdgeCollider2D>();
		EdgeCollider2D bottom = gameObject.AddComponent<EdgeCollider2D>();

		top.points = new Vector2[] { new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f) };
		bottom.points = new Vector2[] { new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f) };
	}
	
}
