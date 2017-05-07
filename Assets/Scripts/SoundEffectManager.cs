using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager> {

	public enum SoundEffectChoice { Null, Fire, CannonFire, MenuSelect, Quack, Deflate };
	public GameObject SoundEffectPrefab;

	public AudioClip CannonFire1Clip;
	public AudioClip CannonFire2Clip;
	public AudioClip MenuSelectClip;
	public AudioClip Quack1Clip;
	public AudioClip Quack2Clip;
	public AudioClip Quack3Clip;
	public AudioClip DeflateClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PlaySoundEffect(SoundEffectChoice effect){
		SoundEffect soundEffect = Instantiate(SoundEffectPrefab, transform.position, Quaternion.identity).GetComponent<SoundEffect>();
		
		switch(effect){
			case SoundEffectChoice.CannonFire:
				if((int)Random.Range(0, 2) == 0){
					soundEffect.Play(CannonFire1Clip);
				} else {
					soundEffect.Play(CannonFire2Clip);
				}

			break;
			
			case SoundEffectChoice.MenuSelect:
				soundEffect.Play(MenuSelectClip);
			break;

			case SoundEffectChoice.Quack:
				int selection = (int)Random.Range(0, 3);
				if(selection == 0){
					soundEffect.Play(Quack1Clip);
				} else if(selection == 1){
					soundEffect.Play(Quack2Clip);
				} else if(selection == 2){
					soundEffect.Play(Quack3Clip);
				}
			break;

			case SoundEffectChoice.Deflate:
				soundEffect.Play(DeflateClip);
			break;

			default:
				soundEffect.Play(null);
			break;
		}
		
	}
}
