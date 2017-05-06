using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Controllers/HarpoonController")]

[RequireComponent(typeof(Rigidbody))]
public class HarpoonController : MonoBehaviour {


	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		Fire(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Fire(int playerNum){
		rb.AddForce(transform.forward * 100);
	}
}
