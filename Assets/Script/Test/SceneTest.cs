using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene ("game", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
