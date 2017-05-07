using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager> {

	public AudioSource[] IntroLoopSections;
	public AudioSource MainGameMusic;
	public bool MainGameMusicIsQueued;
	public bool MainGameMusicIsPlaying;
	public int LastLoopSection;
	public int CurrentLoopSection;
	public int NextLoopSection;
	public double initialAudioSettingsTime;

	double audioTimer = 0;

	// Use this for initialization
	void Start () {
		CurrentLoopSection = -1;
		MainGameMusicIsQueued = false;
		MainGameMusicIsPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
		QueueNextSection();
		if(Input.GetButton("Jump")){
			MainGameMusicIsQueued = true;
		}
	}

	public void PlayMainGameMusic(){
		MainGameMusicIsQueued = true;
	}

	public void QueueNextSection(){
		if(CurrentLoopSection == -1){ //If we're queueing for the first time,
			initialAudioSettingsTime = AudioSettings.dspTime; //Set the initial first time
			audioTimer = 0;
			IntroLoopSections[0].Play(); //Play section 1

			LastLoopSection = -1;
			CurrentLoopSection = 0;
			NextLoopSection = 1;
		} else {
			if(IntroLoopSections[CurrentLoopSection].isPlaying){ //If the current section is playing, IE: We don't need to transition next,
				if(!MainGameMusicIsPlaying){ //If the MainGameMusic isn't playing
					//Queue up the next section
					IntroLoopSections[NextLoopSection].PlayScheduled(initialAudioSettingsTime + audioTimer + IntroLoopSections[CurrentLoopSection].clip.length);
				}

				if(MainGameMusicIsQueued){ //If we're queued up to play the main game music next, 
					MainGameMusic.PlayScheduled(initialAudioSettingsTime + audioTimer + IntroLoopSections[CurrentLoopSection].clip.length); //Queue it up as the next track,
					IntroLoopSections[NextLoopSection].Stop(); //Don't queue up the track we just queued up
					MainGameMusicIsQueued = false;
					MainGameMusicIsPlaying = true;
				}
			} else { //Otherwise, we need to change the sections for the next audio clip
				if(!MainGameMusicIsPlaying){
					LastLoopSection = CurrentLoopSection; //Set variables
					CurrentLoopSection = NextLoopSection;
					NextLoopSection ++;

					if(NextLoopSection == IntroLoopSections.Length){ //If we're out of bounds,
						LastLoopSection = IntroLoopSections.Length-2;
						CurrentLoopSection = IntroLoopSections.Length-1;
						NextLoopSection = 0;
					}

					Debug.Log("C: " + CurrentLoopSection);
					Debug.Log("N: " + NextLoopSection);

					audioTimer += IntroLoopSections[NextLoopSection].clip.length;
				}
			}
		}
	}
}
