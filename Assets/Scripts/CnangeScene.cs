using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CnangeScene : MonoBehaviour
{


    public void LoadSceneHammer()
    {
        SceneManager.LoadScene("Hammer");
    }
    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadScenePlane()
    {
        SceneManager.LoadScene("Plane");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
