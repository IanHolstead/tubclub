using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () { //On start,
		SceneManager.LoadScene("MainMenu"); //Load the main menu
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
