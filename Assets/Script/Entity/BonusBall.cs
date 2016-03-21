using UnityEngine;
using System.Collections;

public class BonusBall : MonoBehaviour {
	
	void Start () {
	}

	void Update () {
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log (other.tag);
		if (other.CompareTag("Ball")) {
			gameObject.SetActive (false);
			Destroy (gameObject);
		}
	}
}
