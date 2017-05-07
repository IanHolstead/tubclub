using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager> {

	public enum SoundEffectChoice { Null, Fire };
	public GameObject SoundEffectPrefab;

	public AudioClip FireClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Jump"))
		PlaySoundEffect(SoundEffectChoice.Fire);
	}

	public void PlaySoundEffect(SoundEffectChoice effect){
		SoundEffect soundEffect = Instantiate(SoundEffectPrefab, transform.position, Quaternion.identity).GetComponent<SoundEffect>();
		
		switch(effect){
			default:
				soundEffect.Play(null);
			break;
		}
		
	}
}
