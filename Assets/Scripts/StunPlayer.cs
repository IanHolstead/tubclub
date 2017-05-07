using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        print("test");
        if (other.tag == "Player")
        {
            PlayerController hitPlayer = other.GetComponent<PlayerController>();
            if (hitPlayer != null)
            {
                hitPlayer.Stun();
                print("STUN!"); 
            }
        }
    }
}
