using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void ContinueButton()
    {
        SceneManager.LoadScene("OverWorld");
    }

    public void NewWorldButton()
    {
        SceneManager.LoadScene("OverWorld");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Quiting the game");
        }
        #else
        Application.Quit();
        #endif

    }
}
