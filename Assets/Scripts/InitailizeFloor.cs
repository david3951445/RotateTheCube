using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;

public class InitailizeFloor : MonoBehaviour
{
    public GameObject Cube; // The cube
    public GameObject GridPrefab; // A grid

    private const int FLOOR_SIZE = 3; // Floor size = 4*4
    private GameObject[,] floor = new GameObject[FLOOR_SIZE, FLOOR_SIZE]; // Store grid

    void Start() {
        Material[] faceMaterials = Cube.GetComponent<MeshRenderer>().materials; // Get cube 6 faces' materail

        for (int i = 0; i < FLOOR_SIZE; i++) {
            for (int j = 0; j < FLOOR_SIZE; j++) {
                floor[i, j] = Instantiate(GridPrefab); 
                floor[i, j].transform.position = new Vector3(i, 0, j);
                floor[i, j].transform.parent = this.transform;
                floor[i, j].GetComponent<MeshRenderer>().material = faceMaterials[Random.Range(0, faceMaterials.Length)];

                GameObject flipFloor = Instantiate(GridPrefab);
                flipFloor.transform.position = new Vector3(i, 0, j);
                flipFloor.transform.rotation = Quaternion.Euler(180, 0, 0);
                flipFloor.transform.parent = this.transform;
                flipFloor.GetComponent<MeshRenderer>().material = faceMaterials[Random.Range(0, faceMaterials.Length)];
            }

        }


    }
    void Update() {

    }
    void SetColor(ProBuilderMesh pMesh) {
        Debug.Log(pMesh.colors[1].ToString());
        // Cycle through each unique vertex in the cube (8 total), and assign a color
        // to the index in the sharedIndices array.
        int sharedVertexCount = pMesh.sharedVertices.Count;

        Color[] vertexColors = new Color[sharedVertexCount];

        for (int i = 0; i < sharedVertexCount; i++) {
            //vertexColors[i] = Color.red;

            vertexColors[i] = Color.HSVToRGB((i / (float)sharedVertexCount) * 360f, 1f, 1f);
        }

        // Now go through each face (vertex colors are stored the pb_Face class) and
        // assign the pre-calculated index color to each index in the triangles array.
        var colors = pMesh.colors;

        for (int sharedIndex = 0; sharedIndex < pMesh.sharedVertices.Count; sharedIndex++) {
            foreach (int index in pMesh.sharedVertices[sharedIndex]) {
                colors[index] = vertexColors[sharedIndex];
            }
        }

        pMesh.colors = colors;

        // In order for these changes to take effect, you must refresh the mesh
        // object.
        pMesh.Refresh();
    }
}
