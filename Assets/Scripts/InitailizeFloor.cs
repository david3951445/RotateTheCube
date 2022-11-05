using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;

public class InitailizeFloor : MonoBehaviour
{
    public GameObject Cube; // The cube
    public GameObject FacePrefab; // A grid

    private int NumOfFaces; // Number of faces of floor
    private int FLOOR_SIZE = 3; // Floor size = 4*4
    private GameObject[,] floor; // Store grid

    private int numOfFacesCounter = 0;
    public int NumOfFacesCounter {
        get { return numOfFacesCounter; }
        set {
            numOfFacesCounter = value;
            Debug.Log(NumOfFacesCounter);
        }
    }
    void Start() {

        Material[] faceMaterials = Cube.GetComponent<MeshRenderer>().materials; // Get cube 6 faces' materail
        floor = new GameObject[FLOOR_SIZE, FLOOR_SIZE];
        NumOfFaces = FLOOR_SIZE * FLOOR_SIZE;

        for (int i = 0; i < FLOOR_SIZE; i++) {
            for (int j = 0; j < FLOOR_SIZE; j++) {
                floor[i, j] = Instantiate(FacePrefab); 
                floor[i, j].transform.position = new Vector3(i, 0, j);
                floor[i, j].transform.parent = this.transform;
                floor[i, j].GetComponent<MeshRenderer>().material = faceMaterials[Random.Range(0, faceMaterials.Length)];

                //GameObject flipFloor = Instantiate(FacePrefab);
                //flipFloor.transform.position = new Vector3(i, 0, j);
                //flipFloor.transform.rotation = Quaternion.Euler(180, 0, 0);
                //flipFloor.transform.parent = this.transform;
                //flipFloor.GetComponent<MeshRenderer>().material = faceMaterials[Random.Range(0, faceMaterials.Length)];
            }
        }


    }
    void Update() {
    }
}
