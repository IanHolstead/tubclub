using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour, FloatyInterface {
    float weight;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecieveForce(Vector3 force)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetLocation()
    {
        return gameObject.transform.position;
    }

    public Vector3 GetVelocity()
    {
        return GetComponent<Rigidbody>().velocity;
    }
}
