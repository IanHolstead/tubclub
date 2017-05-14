using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpawner : Singleton<FloatySpawner> {

	[Header("Spawn Variables")]
	public GameObject[] FloatyPrefabs;
	public GameObject FloatySkeletonPrefab;
	public bool IsSpawning;
	public float SpawnRate;
	public float MinSpawnRadius;
	public float MaxSpawnRadius;

	float spawnTimer;

	// Use this for initialization
	void Start () {
		spawnTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsSpawning){
			spawnTimer += Time.deltaTime;
			if(spawnTimer >= SpawnRate){
				spawnTimer = spawnTimer - SpawnRate;
				SpawnNewFloaty();
			}
		}
	}

	public void SpawnNewFloaty(){
		GameObject newFloaty = Instantiate(FloatyPrefabs[GetRandomFloatyIndex()]) as GameObject;
		
		Vector3 displacement = newFloaty.transform.position;
		Vector3 spawnPoint = GetRandomSpawnPoint();

		float x = Random.Range(0, 360);
		float y = Random.Range(0, 360);
		float z = Random.Range(0, 360);
		newFloaty.transform.eulerAngles = new Vector3(x, y, z);

		

		newFloaty.transform.position = spawnPoint + displacement;

		GameObject newFloatySkeleton = Instantiate(FloatySkeletonPrefab, spawnPoint, Quaternion.identity) as GameObject;
		newFloaty.transform.SetParent(newFloatySkeleton.transform.GetChild(0));
	}

	public int GetRandomFloatyIndex(){
		return Random.Range(0, FloatyPrefabs.Length);
	}

	public Vector3 GetRandomSpawnPoint(){
		float angle = Random.Range(0, Mathf.PI * 2);
		float radius = Random.Range(MinSpawnRadius, MaxSpawnRadius);
		float x = Mathf.Cos(angle)*radius + transform.position.x;
		float y = transform.position.y;
		float z = Mathf.Sin(angle)*radius + transform.position.z;
		return new Vector3(x, y, z);
	}

	//private void OnDrawGizmos(){
	//	UnityEditor.Handles.color = Color.red;
	//	UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, MinSpawnRadius);
	//	UnityEditor.Handles.color = Color.green;
	//	UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, MaxSpawnRadius);
	//}
}
