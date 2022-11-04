using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FaceCollision : MonoBehaviour
{
    public Material noColorMaterial;
    public bool isTriggered = false;
    private bool isRotating;
    // Start is called before the first frame update
    void Start()
    {
        isRotating = GetComponentInParent<FaceCubeBehaviour>().isRotating;
    }

    //void OnCollisionEnter(Collision collision) {
    //    Debug.Log("Collide");
    //    foreach (ContactPoint contact in collision.contacts) {
    //        Debug.DrawRay(contact.point, contact.normal, Color.white);
    //    }
    //}
    private void OnTriggerEnter(Collider other) {
        MeshRenderer mr = other.gameObject.GetComponent<MeshRenderer>();
        Material material = other.gameObject.GetComponent<MeshRenderer>().sharedMaterial;    
        if (GetComponent<MeshRenderer>().material.name == mr.sharedMaterial.name) { // Same color
            // Remove the floor color
            //Debug.Log(mr.sharedMaterial.name);
            //Debug.Log(noColorMaterial.name);
            mr.material = noColorMaterial;
            GetComponentInParent<InitailizeFloor>().NumOfFacesCounter++;
            Debug.Log(GetComponentInParent<InitailizeFloor>().NumOfFacesCounter);
        }
    }
}
