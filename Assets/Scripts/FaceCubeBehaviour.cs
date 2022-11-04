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
    // [Right  Left Up  Down  Forward Back  ]
    // [Orange Blue Red Green Purple  Yellow]
    private FaceCollision[] faces; 

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
            if (Input.GetKey(KeyCode.RightArrow)) {
                supportPoint = transform.localPosition + new Vector3(halfSize, -halfSize, 0);
                supportEdge = Vector3.forward;
                startMove();
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                supportPoint = transform.localPosition + new Vector3(-halfSize, -halfSize, 0);
                supportEdge = Vector3.back;
                startMove();
            }
            if (Input.GetKey(KeyCode.UpArrow)) { // Forward
                supportPoint = transform.localPosition + new Vector3(0, -halfSize, halfSize);
                supportEdge = Vector3.left;
                startMove();
            }
            if (Input.GetKey(KeyCode.DownArrow)) { // Back
                supportPoint = transform.localPosition + new Vector3(0, -halfSize, -halfSize);
                supportEdge = Vector3.right;
                startMove();
            }
        }
        else {
            //Debug.Log(GetComponentInChildren<FloorCollision>().isTriggered);
            Rotate90();
        }

    }

    private void startMove() {
        isRotating = true;
        MoveText.GetComponent<TextMeshProUGUI>().text = $"Move: {++moveCounter}";
    }

    void Rotate90() {
        float delta = DegreePerSecond * Time.deltaTime;
        rotatedAngle += delta;
        if (rotatedAngle - 90 >= 0) { // Exceed 90, rotate finish
            delta -= rotatedAngle - 90; // Make it not exceed 90
            rotatedAngle = 0;
            isRotating = false;
        }
        transform.RotateAround(supportPoint, supportEdge, -delta);
    }

    // The current 
    // Axis  | x       -x    y    -y     z       -z
    // Color | Orange  Blue  Red  green  Purple  Yellow
    // Num   | 1       2     3    4      5       6
    private int[] cubeState = { 1, 2, 3, 4, 5, 6 };

    // Possible permutations (4 rotations of cube w.r.t. x and z axis)
    // +x (3645), -x (3546), +z (3142), -z(4132)
    void Permute() {
        // updaate new cube state

        // return the color that face the floor to determine if cube can eliminate
    }

}
