using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void WinterButton()
    {
        SceneManager.LoadScene("SnowWorld");
    }

    public void SummerButton()
    {
        SceneManager.LoadScene("SummerWorld");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContiueButton()
    {
        SceneManager.LoadScene("SnowWorld");
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
