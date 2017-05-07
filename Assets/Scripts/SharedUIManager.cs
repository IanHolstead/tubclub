using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SharedUIManager : Singleton<SharedUIManager> {

	[Header("Object References")]
	public TimerController timerController;
	public GameOverController gameOverMenu;
	public RectTransform CircleTransitioner;
	public TextMeshProUGUI EndGameText;

	void Update(){
	}

	public void SetEndGameText(string text){
		EndGameText.text = text;
	}

	public void ShowTimer(){
		timerController.gameObject.SetActive(true);
	}

	public void HideTimer(){
		timerController.gameObject.SetActive(false);
	}

	public void UpdateTimer(float timer, float maxTime){
		timerController.UpdateTimer(timer, maxTime);
	}

	public void ShowGameOverMenu(){
		gameOverMenu.Show();
	}

	public void SetCircleTransitioner(float val){
		CircleTransitioner.sizeDelta = new Vector2(CircleTransitioner.sizeDelta.x, 2000 * val);
	}

	public void HideCircleTransitioner(){
		CircleTransitioner.sizeDelta = new Vector2(CircleTransitioner.sizeDelta.x, 0);
	}
}
