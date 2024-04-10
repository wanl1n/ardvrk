using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenImgur()
    {
        Debug.Log("Get Image");
        Application.OpenURL("https://imgur.com/a/baGb6dI");
    }

    public void Continue()
    {
        Debug.Log("Continue");
        SceneManager.LoadScene("MainScene");
    }

    public void GoToNavigationMode()
    {
        SceneManager.LoadScene("Navigation");
    }
    public void GoToTourMode()
    {
        SceneManager.LoadScene("MiniTour");
    }
}
