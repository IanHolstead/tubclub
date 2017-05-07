using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedUIManager : Singleton<SharedUIManager> {

	[Header("Object References")]
	public TimerController timerController;
	public GameOverController gameOverMenu;
	public RectTransform CircleTransitioner;

	void Update(){
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
