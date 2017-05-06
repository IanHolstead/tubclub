using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpinner : MonoBehaviour {

    [Tooltip("In RPM")]
    public float spinSpeed = 10;

    float angle = 360;

	void Update () {
        angle = (angle - Time.deltaTime * spinSpeed * 360 / 60) % (360);

        Vector3 rotation = transform.eulerAngles;
        rotation.y = angle;
        transform.rotation = Quaternion.Euler(rotation);
	}
}
