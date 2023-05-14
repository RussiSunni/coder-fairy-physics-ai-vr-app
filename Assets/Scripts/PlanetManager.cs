using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetManager : MonoBehaviour
{    
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "The Earth")
        {
            // Do something...
        }
        else if (sceneName == "The Moon")
        {
            Physics.gravity = new Vector3(0, -1.0F, 0);
        }
    }   
}
