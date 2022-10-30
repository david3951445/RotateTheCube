using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CubeBehaviour : MonoBehaviour
{
    public float DegreePerSecond = 90; // Rotate speed

    private bool isRotating = false;
    private float size; // Cube size
    private float halfSize; // Half Cube size
    private float rotatedAngle = 0; // Rotated angle of cube when rotating

    // 4 rotate axises are the 4 stand edges of cube
    private Vector3 standPoint;
    private Vector3 standEdge = new Vector3();

    void Start() {
        size = transform.localScale.x;
        halfSize = size / 2;
        transform.position = new Vector3(0, halfSize, 0);
    }

    void Update() {
        //Debug.DrawRay(startPoint, 2 * (endPoint - startPoint));
        if (!isRotating) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                standPoint = transform.localPosition + new Vector3(0, -halfSize, halfSize);
                standEdge = Vector3.left;
                isRotating = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                standPoint = transform.localPosition + new Vector3(-halfSize, -halfSize, 0);
                standEdge = Vector3.back;
                isRotating = true;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                standPoint = transform.localPosition + new Vector3(0, -halfSize, -halfSize);
                standEdge = Vector3.right;
                isRotating = true;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                standPoint = transform.localPosition + new Vector3(halfSize, -halfSize, 0);
                standEdge = Vector3.forward;
                isRotating = true;
            }
        }
        else {
            Rotate90();
        }

    }
    void Rotate90() {
        float delta = DegreePerSecond * Time.deltaTime;
        rotatedAngle += delta;
        if (rotatedAngle - 90 >= 0) { // Exceed 90
            delta -= rotatedAngle - 90; // Make it not exceed 90
            rotatedAngle = 0;
            isRotating = false;
        }
        transform.RotateAround(standPoint, standEdge, -delta);
    }

}
