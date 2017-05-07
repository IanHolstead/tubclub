using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	[Header("Component References")]
	public CanvasGroup GameOverCanvasGroup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToMainMenu(){
		GameManager.Instance.LoadLevel("MainMenu");
	}

	public void Show(){
		GameOverCanvasGroup.alpha = 1;
	}
}
