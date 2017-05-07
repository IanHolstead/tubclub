using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			PlayerController hitPlayer = other.GetComponent<PlayerController>();
			if(hitPlayer != null){
				hitPlayer.Stun();
			}
		}
	}
}
