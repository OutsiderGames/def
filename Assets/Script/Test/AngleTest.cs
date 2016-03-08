using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngleTest : MonoBehaviour {

	public Text text;
	public Transform a;
	public Transform b;
	
	void Update () {
		Vector3 dir = a.position - b.position;

		text.text = "Angle : " + Vector3.Angle(a.position, b.position)
			+ "\nDistance : " + Vector3.Distance(a.position, b.position)
			+ "\nDirection : " + dir
			+ "\nDot Prod : " + Vector3.Dot(a.position.normalized, b.position.normalized)
			;
		
	}
}
