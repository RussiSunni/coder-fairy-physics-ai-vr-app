using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{    
    public void EarthStage()
    {
        SceneManager.LoadScene("The Earth");
    }
    public void MoonStage()
    {
        SceneManager.LoadScene("The Moon");
    }
}
