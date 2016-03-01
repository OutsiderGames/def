using UnityEngine;
using System.Collections;

public class VerticalBrickCollider : MonoBehaviour {

	void Start () {
		EdgeCollider2D left = gameObject.AddComponent<EdgeCollider2D>();
		EdgeCollider2D right = gameObject.AddComponent<EdgeCollider2D>();

		left.points = new Vector2[] { new Vector2(-0.5f, 0.5f), new Vector2(-0.5f, -0.5f) };
		right.points = new Vector2[] { new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f) };
	}
}