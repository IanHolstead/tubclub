using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Managers/AudioManager")]

[RequireComponent(typeof(AudioListener))]
public class AudioManager : Singleton<AudioManager> {

    AudioListener audioListener;

    void Awake(){
        DontDestroyOnLoad(transform.gameObject); //Don't destroy us on loading new scenes
        audioListener = GetComponent<AudioListener>();
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}
