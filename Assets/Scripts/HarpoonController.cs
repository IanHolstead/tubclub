using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Controllers/HarpoonController")]

[RequireComponent(typeof(Rigidbody))]
public class HarpoonController : MonoBehaviour {

	int firedBy;
	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Fire(int playerNum, Vector3 direction){
		firedBy = playerNum;
		rb.AddForce(direction.normalized * 3000);
	}
}
