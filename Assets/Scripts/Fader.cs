using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour {

	CanvasGroup cg;
	float fadeTime;
	float fadeTimer;
	bool isFading;

	void Awake(){
		cg = GetComponent<CanvasGroup>();
		isFading = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isFading){
			cg.alpha = fadeTimer/fadeTime;
			fadeTimer += Time.deltaTime;
			if(fadeTimer >= fadeTime){
				isFading = false;
				cg.alpha = 1;
			}
		}
	}

	public void Fade(float time){
		fadeTime = time;
		fadeTimer = 0;
		isFading = true;
	}
}
