using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : Singleton<MainMenuManager> {

	[Header("Canvas References")]
	public CanvasGroup Fader;
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
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);
		MainMenuCanvas.SetActive(false); //Hide main menu
		CreditsCanvas.SetActive(true); //Show credits
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Back Button")); //Find and select back button
	}
	
	public void HideCredits(){
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);
		MainMenuCanvas.SetActive(true); //Show main menu
		CreditsCanvas.SetActive(false); //Hide credits
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Credits Button")); //Find and select credits button
	}
	
	public void ExitGame(){
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; //Stop playing if in editor
		#endif

		Application.Quit(); //Quit game if in application
	}
	
	public void ShowPlayerSelect(){
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);
		MainMenuCanvas.SetActive(false); //Hide main menu
		PlayerSelectCanvas.SetActive(true); //Show player select
		EventSystem.current.SetSelectedGameObject(GameObject.Find("2 Player Button")); //Find and select 2 player button
	}
	
	public void HidePlayerSelect(){
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);
		MainMenuCanvas.SetActive(true); //Show main menu
		PlayerSelectCanvas.SetActive(false); //Hide player select
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Play Button")); //Find and select play button
	}
	
	public void StartGame(int players){ //Start game with given # of players
		SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectChoice.MenuSelect);
		MusicManager.Instance.PlayMainGameMusic();
		GameManager.Instance.StartGame(players); //Tell teh game manager instance to start the game
	}

	public void SetFaderAlpha(float alpha){
		Fader.alpha = alpha;
	}
}
