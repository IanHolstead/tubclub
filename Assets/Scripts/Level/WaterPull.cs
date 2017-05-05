using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPull : MonoBehaviour {

    HashSet<FloatyInterface> objects;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        foreach (FloatyInterface item in objects)
        {

        }
	}

    public bool AddObject(MonoBehaviour objectToAdd)
    {
        if (objectToAdd is FloatyInterface)
        {
            return objects.Add(objectToAdd as FloatyInterface);
        }
        return false;
    }


    Vector3 CalculateForce(FloatyInterface floaty)
    {
        float radius = GetHorizontalVector(floaty.GetLocation()).magnitude;
        if (radius < 10)
        {
            return Vector3.zero;
        }

        return new Vector3();
    }

    public static Vector3 GetHorizontalVector(Vector3 vector)
    {
        vector.y = 0;
        return vector;
    }
}
