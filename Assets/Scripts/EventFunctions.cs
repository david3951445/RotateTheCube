using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EventFunctions : MonoBehaviour
{
    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
