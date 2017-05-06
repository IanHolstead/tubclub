using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {
    public GameObject meshRef;
    public float bobRate = .4f;
    public float bobAmount = 2;

    float age;
	void Start () {
        age = Random.Range(0f, 1000f);
	}
	
	void Update () {
        age += Time.deltaTime;

        RaycastHit hit;

        //TODO: this hits itself
        if (Physics.Raycast(transform.position + Vector3.up*10, Vector3.up*-1, out hit, 100)){
            Vector3 pos = transform.position;
            pos.y = hit.point.y + BobUpAndDown();
            transform.position = pos;
        }
	}

    float BobUpAndDown()
    {
        return bobAmount * Mathf.Sin((age * bobRate) * Mathf.PI * 2);
    }
}
