using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = FindObjectOfType<TransitionManager>();
    }

    public void ContinueButton()
    {
        transitionManager.FadeToLevel(3);
    }

    public void NewWorldButton()
    {
        PlayerPrefs.SetFloat("X", 0.0f);
        PlayerPrefs.SetFloat("Y", 1.0f);
        PlayerPrefs.SetFloat("Z", 0.0f);
        transitionManager.FadeToLevel(3);
    }

    public void MainMenuButton()
    {
        transitionManager.FadeToLevel(1);
    }

    public void CreditsButton()
    {
        transitionManager.FadeToLevel(2);
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
