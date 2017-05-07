using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpawner : MonoBehaviour {

	[Header("Spawn Variables")]
	public GameObject[] FloatyPrefabs;
	public float SpawnRate;
	public float MinSpawnRadius;
	public float MaxSpawnRadius;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetRandomSpawnPoint(){
		float angle = Random.Range(0, Mathf.PI * 2);
		float radius = Random.Range(MinSpawnRadius, MaxSpawnRadius);
		return new Vector3(Mathf.Cos(angle)*radius, transform.position.y, Mathf.Sin(angle)*radius);
	}

	private void OnDrawGizmos(){
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, MinSpawnRadius);
		UnityEditor.Handles.color = Color.green;
		UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, MaxSpawnRadius);
	}
}
