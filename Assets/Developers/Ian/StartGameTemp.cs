using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance.state = GameManager.State.Playing;
        GameManager.Instance.SpawnPlayers(GameManager.Instance.numberOfPlayers);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
