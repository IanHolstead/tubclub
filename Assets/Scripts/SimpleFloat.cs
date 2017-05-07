using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFloat : MonoBehaviour {

	public float bobRate;
	public float bobAmount;


	Vector3 origin;
	float time;

	// Use this for initialization
	void Start () {
		time = Random.Range(0f, 1000f);
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		float height = bobAmount/2 * Mathf.Sin((time * bobRate) * Mathf.PI * 2) - bobAmount/2;
		height *= -1;
		Debug.Log(height);

		transform.position = new Vector3(origin.x, origin.y + height, origin.z);
	}
}
