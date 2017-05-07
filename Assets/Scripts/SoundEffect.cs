using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundEffect : MonoBehaviour {

	AudioSource source;
	bool isPlaying;

	void Awake(){
		isPlaying = false;
		source = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying && !source.isPlaying){
			Destroy(gameObject);
		}
	}

	public void Play(AudioClip clip){
		if(clip == null){
			Destroy(gameObject);
			return;
		}
		source.clip = clip;
		source.Play();
		isPlaying = true;
	}
}
