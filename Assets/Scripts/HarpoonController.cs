using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Controllers/HarpoonController")]

[RequireComponent(typeof(Rigidbody))]
public class HarpoonController : MonoBehaviour {

	public int firedBy;
	Rigidbody rb;

    float age;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	void Update(){
		transform.rotation = Quaternion.LookRotation(rb.velocity);
        age += Time.deltaTime;
        if (age > 2f)
        {
            Destroy(gameObject);
        }
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			PlayerController hitPlayer = other.GetComponent<PlayerController>();
			if(hitPlayer != null){
				if(hitPlayer.PlayerNum != firedBy){
					hitPlayer.HitByHarpoon();
					Destroy(gameObject);
				} else {
					return;
				}
			}
		} else {
			Destroy(gameObject);
		}
	}

	public void Fire(int playerNum, Vector3 direction, float initialVelocityMagnitude){
		firedBy = playerNum;
		rb.AddForce(direction.normalized * initialVelocityMagnitude, ForceMode.VelocityChange);
	}
}
