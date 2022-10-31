using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collide");
        foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
