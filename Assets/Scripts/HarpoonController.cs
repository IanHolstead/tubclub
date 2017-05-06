using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Controllers/HarpoonController")]

[RequireComponent(typeof(Rigidbody))]
public class HarpoonController : MonoBehaviour {

	public int firedBy;
	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			PlayerController hitPlayer = other.GetComponent<PlayerController>();
			if(hitPlayer != null){
				if(hitPlayer.PlayerNum != firedBy){
					hitPlayer.HitByHarpoon();
				}
			}
		}
	}

	public void Fire(int playerNum, Vector3 direction){
		firedBy = playerNum;
		rb.AddForce(direction.normalized * 3000);
	}
}
