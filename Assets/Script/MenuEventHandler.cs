using UnityEngine;
using System.Collections;

public class MenuEventHandler : MonoBehaviour {
	public void OnHelp() {
		Debug.Log ("Hey, are you looking for helper?");
	}

	public void OnPause() {
		Debug.Log ("I also would like to stop.. but.. not now");
	}

	public void OnSettings() {
		Debug.Log ("Now we preparing settings menu, please wait a moment");
	}
}
