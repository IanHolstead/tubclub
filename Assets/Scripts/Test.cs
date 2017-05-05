using UnityEngine;
using System.Collections;
using ControlWrapping;

public class Test : MonoBehaviour {
    Rigidbody rb;
    float g;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.right * 2;
        g = Mathf.Pow(rb.velocity.magnitude, 2) * transform.position.magnitude;
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Logger.Log("test"); 
            Debug.Log(g);
        }

    }
}
