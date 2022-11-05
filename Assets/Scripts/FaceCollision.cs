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
    //    isTriggered = true;
    //    //Debug.Log(isTriggered);
    //    MeshRenderer mr = collision.gameObject.GetComponent<MeshRenderer>();
    //    Debug.Log(GetComponent<MeshRenderer>().material.name);
    //    Debug.Log(mr.sharedMaterial.name);
    //    if (GetComponent<MeshRenderer>().material.name == mr.sharedMaterial.name) { // Same color
    //        // Remove the floor color
    //        //Debug.Log(mr.sharedMaterial.name);
    //        //Debug.Log(noColorMaterial.name);
    //        mr.material = noColorMaterial;
    //        GetComponentInParent<InitailizeFloor>().NumOfFacesCounter++;
    //        Debug.Log(GetComponentInParent<InitailizeFloor>().NumOfFacesCounter);
    //    }
    //}

    private void OnTriggerEnter(Collider other) {
        isTriggered = true;
        MeshRenderer mr = other.gameObject.GetComponent<MeshRenderer>();
        if (GetComponent<MeshRenderer>().material.name == mr.sharedMaterial.name) { // Same color
            // Remove the floor color
            //Debug.Log(mr.sharedMaterial.name);
            //Debug.Log(noColorMaterial.name);
            mr.material = noColorMaterial;
            GetComponentInParent<InitailizeFloor>().NumOfFacesCounter++;
        }
    }
}
