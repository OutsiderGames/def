using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DebugSceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene ("game", LoadSceneMode.Additive);
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}
}
