using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class FaceCubeBehaviour : MonoBehaviour
{
    public float DegreePerSecond = 90; // Rotate speed
    public GameObject MoveText;
    public bool isRotating = false;

    private float size; // Cube size
    private float halfSize; // Half Cube size
    private float rotatedAngle = 0; // Rotated angle of cube when rotating
    private int moveCounter = 0;

    // 4 rotate axises are the 4 support edges of cube
    private Vector3 supportPoint;
    private Vector3 supportEdge = new Vector3();

    // Childs
    // Cube rotation direction |    Right   Left    Up      Down    Forward Back    
    // Color of faces          |    Orange  Blue    Red     Green   Purple  Yellow  
    // Number of faces         |    0       1       2       3       4       5       
    // Normal vector of faces  |    +x      -x      +y      -y      +z      -z      
    private FaceCollision[] faces;
    private int[] facesNumbers = { 0, 1, 2, 3, 4, 5 }; // Can omit and use faces[]
    private int facesIndex = 0;

    void Start() {
        size = transform.localScale.x;
        //Debug.Log(size);
        halfSize = size / 2;
        transform.position = new Vector3(0, halfSize, 0);

        faces = GetComponentsInChildren<FaceCollision>();
    }

    void Update() {
        //Debug.DrawRay(startPoint, 2 * (endPoint - startPoint));
        if (!isRotating) {
            if (Input.GetKey(KeyCode.D)) { // Right
                supportPoint = transform.localPosition + new Vector3(halfSize, -halfSize, 0);
                supportEdge = Vector3.forward;
                facesIndex = 0;
                startMove();
            }
            if (Input.GetKey(KeyCode.A)) { // Left
                supportPoint = transform.localPosition + new Vector3(-halfSize, -halfSize, 0);
                supportEdge = Vector3.back;
                facesIndex = 1;
                startMove();
            }
            if (Input.GetKey(KeyCode.Q)) { // Up
                Debug.Log("You press up");
            }
            if (Input.GetKey(KeyCode.E)) { // Down
                Debug.Log("You press down");
            }
            if (Input.GetKey(KeyCode.W)) { // Forward
                supportPoint = transform.localPosition + new Vector3(0, -halfSize, halfSize);
                supportEdge = Vector3.left;
                facesIndex = 4;
                startMove();
            }
            if (Input.GetKey(KeyCode.S)) { // Back
                supportPoint = transform.localPosition + new Vector3(0, -halfSize, -halfSize);
                supportEdge = Vector3.right;
                facesIndex = 5;
                startMove();
            }

        }
        else {
            //Debug.Log(GetComponentInChildren<FloorCollision>().isTriggered);
            Rotate90();
        }

    }

    private void startMove() {
        // Unvalid rotate

        // Valid rotate
        //RotateFacesNumbers();
        isRotating = true;
        faces[facesNumbers[facesIndex]].isTriggered = false;
        MoveText.GetComponent<TextMeshProUGUI>().text = $"Move: {++moveCounter}";
    }

    private void Rotate90() {
        float offsetAngle = DegreePerSecond * Time.deltaTime;
        rotatedAngle += offsetAngle;
        if (faces[facesNumbers[facesIndex]].isTriggered) { // Collide the face, rotate finish
            offsetAngle -= rotatedAngle - 90; // Make it not exceed 90
            rotatedAngle = 0;
            isRotating = false;
            RotateFacesNumbers();
            //foreach (var item in facesNumbers) {
            //    Debug.Log(item.ToString());
            //}

        }
        transform.RotateAround(supportPoint, supportEdge, -offsetAngle);
    }

    // Possible permutations (4 rotations of cube w.r.t. x and z axis)
    // x+90 (2534), x-90 (2435), z+90 (0312), z-90(0213)
    private void RotateFacesNumbers() {
        // updaate new cube state, facesNumbers
        switch (facesIndex) {
            case 0: // Right
                Permute(0, 3, 1, 2);
                break;
            case 1: // Left
                Permute(0, 2, 1, 3);
                break;
            case 2: // Up
                Debug.Log("Permute, Up");
                break;
            case 3: // Down
                Debug.Log("Permute, Down");
                break;
            case 4: // Forward
                Permute(2, 4, 3, 5);
                break;
            case 5: // Back
                Permute(2, 5, 3, 4);
                break;
            default:
                break;
        }
    }

    private void Permute(int a, int b, int c, int d) { // Permutation of a permutation group
        int temp = facesNumbers[d];
        facesNumbers[d] = facesNumbers[c];
        facesNumbers[c] = facesNumbers[b];
        facesNumbers[b] = facesNumbers[a];
        facesNumbers[a] = temp;
    }

}
