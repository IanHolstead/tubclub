using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour {

	[Header("Canvas References")]
	public GameObject MainMenuCanvas;
	public GameObject CreditsCanvas;
	public GameObject PlayerSelectCanvas;

	// Use this for initialization
	void Start () {
		GameManager.Instance.state = GameManager.State.MainMenu; //On load, change game state
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowCredits(){
		MainMenuCanvas.SetActive(false); //Hide main menu
		CreditsCanvas.SetActive(true); //Show credits
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Back Button")); //Find and select back button
	}
	
	public void HideCredits(){
		MainMenuCanvas.SetActive(true); //Show main menu
		CreditsCanvas.SetActive(false); //Hide credits
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Credits Button")); //Find and select credits button
	}
	
	public void ExitGame(){

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; //Stop playing if in editor
		#endif

		Application.Quit(); //Quit game if in application
	}
	
	public void ShowPlayerSelect(){
		MainMenuCanvas.SetActive(false); //Hide main menu
		PlayerSelectCanvas.SetActive(true); //Show player select
		EventSystem.current.SetSelectedGameObject(GameObject.Find("2 Player Button")); //Find and select 2 player button
	}
	
	public void HidePlayerSelect(){
		MainMenuCanvas.SetActive(true); //Show main menu
		PlayerSelectCanvas.SetActive(false); //Hide player select
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Play Button")); //Find and select play button
	}
	
	public void StartGame(int players){ //Start game with given # of players
		GameManager.Instance.StartGame(players); //Tell teh game manager instance to start the game
	}
}
