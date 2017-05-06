using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public GameObject meshRef;
    public float bobRate = .75f;
    public float bobAmount = .15f;

    Vector3 hitLoc;

    float age;
    void Start()
    {
        age = Random.Range(0f, 1000f);
    }

    void Update()
    {
        age += Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.up * -1, out hit, 10))
        {

            Vector3 pos = transform.position;
            pos.y = hit.point.y + BobUpAndDown();
            hitLoc = hit.point;
            transform.position = pos;

            //TODO: replace magic number
            float step = 10 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(meshRef.transform.up, hit.normal, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.black);
            Debug.DrawRay(transform.position, meshRef.transform.forward, Color.red);
            Debug.DrawRay(transform.position, Vector3.left, Color.green);
            Debug.DrawRay(transform.position, Vector3.Cross(newDir, Vector3.left), Color.blue);
            Vector3.Cross(newDir, Vector3.left);
            meshRef.transform.rotation = Quaternion.LookRotation(Vector3.Cross(newDir, Vector3.left), newDir);
        }
    }

    //visualize raycast
    private void OnDrawGizmos()
    {
        DebugExtension.DrawArrow(transform.position + Vector3.up, Vector3.up * -1);
        DebugExtension.DrawPoint(transform.position + Vector3.up * -10);
        if (hitLoc != null)
        {
            DebugExtension.DrawPoint(hitLoc, Color.red);
        }
    }

    float BobUpAndDown()
    {
        return bobAmount / 2 * Mathf.Sin((age * bobRate) * Mathf.PI * 2) - bobAmount / 2;
    }
}
