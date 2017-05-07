using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaties : MonoBehaviour {

    [Header("Physics")]
    public AnimationCurve RotationSpeedOverRadius; //Left, 0 | Right, 1 Sample this curve for rotation speed in degrees per second
    public float radius; //Radius of our orbit, distance from 0, 0
    public float radiusVelocity; //Positive => increasing radius
                                 // Use this for initialization

    bool slatedForDestrcution = false;

    public AnimationCurve RadiusReductionOverRadius;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ReduceRadius();
        UpdatePhysics();
        TestRadius();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController hitPlayer = other.GetComponent<PlayerController>();
            if (hitPlayer != null)
            {
                hitPlayer.Stun();
            }
        }
    }

    void UpdatePhysics()
    {
        radius = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2)); //Get radius
        radius += radiusVelocity * Time.deltaTime;
        float angle = Mathf.Atan2(transform.position.x, transform.position.z) * Mathf.Rad2Deg; //Get our angle relative to 0,0
        if (angle < 0)
        { //If our angle is negative, we're under the x-axis
            angle += 360; //Add 360 degrees
        }
        float deltaAngle = RotationSpeedOverRadius.Evaluate(radius); //Sample our curve to get our change in angle
        angle -= deltaAngle;
        angle = angle * Mathf.Deg2Rad; //Convert to radians
        Vector3 newPos = new Vector3(Mathf.Sin(angle) * radius, transform.position.y, Mathf.Cos(angle) * radius); //Calculate their new position
        transform.position = newPos; //Set the new position
        transform.rotation = Quaternion.AngleAxis((angle * Mathf.Rad2Deg) - 90, Vector3.up);
    }

    void ReduceRadius()
    {
        radiusVelocity = RadiusReductionOverRadius.Evaluate(radius);
    }

    void TestRadius()
    {
        if (slatedForDestrcution)
        {
            Vector3 pos = transform.position;
            pos.y -= Time.deltaTime;
            transform.position = pos;
        }
        else if (radius < .9f)
        {
            GetComponent<Float>().enabled = false;
            Destroy(gameObject, 1.5f);
            slatedForDestrcution = true;
        }
    }
}
