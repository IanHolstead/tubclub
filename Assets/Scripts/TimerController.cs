using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour {

	[Header("Object References")]
	public TextMeshProUGUI TimerText;
	public GameObject TimerCircle;

	public void UpdateTimer(float timer, float maxTime){
		TimerText.text = ((float)((int)((maxTime - timer)*10))/10).ToString();
		float timeProportion = timer/maxTime; 
		TimerCircle.transform.localScale = new Vector3(timeProportion, timeProportion, timeProportion);
	}
}
