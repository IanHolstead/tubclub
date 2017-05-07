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
        if(meshRef == null){
            return;
        }
        age += Time.deltaTime;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + .5f * Vector3.up, Vector3.up * -1, 4.5f);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }
                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(pos.y, hit.point.y + BobUpAndDown(), 1f);
                
                hitLoc = hit.point;
                transform.position = pos;
                
                //TODO: replace magic number
                float step = .5f * Time.deltaTime;
                Vector3 newUp = Vector3.RotateTowards(meshRef.transform.up, hit.normal, step, 0.0F);
                Vector3 planeVector = meshRef.transform.position * -1;
                planeVector.y = 0;

                Vector3 newForward = Vector3.Lerp(meshRef.transform.forward, Vector3.Cross(newUp, planeVector), .3f);
                meshRef.transform.rotation = Quaternion.LookRotation(newForward, newUp);
                meshRef.transform.Rotate(0, -10, 0);
            }
        }
    }

    ////visualize raycast
    //private void OnDrawGizmos()
    //{
    //    DebugExtension.DrawArrow(transform.position + .5f * Vector3.up, Vector3.up * -1f);
    //    DebugExtension.DrawPoint(transform.position + Vector3.up * -4.5f);
    //    if (hitLoc != null)
    //    {
    //        DebugExtension.DrawPoint(hitLoc, Color.red);
    //    }
    //}

    float BobUpAndDown()
    {
        return bobAmount / 2 * Mathf.Sin((age * bobRate) * Mathf.PI * 2) - bobAmount / 2;
    }
}
