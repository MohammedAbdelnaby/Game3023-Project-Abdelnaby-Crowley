using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Source: https://www.youtube.com/watch?v=Oadq-IrOazg help with Transistions, also used it in the Transistion EX 9
public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private int LevelToLoad;

    public void FadeToLevel(int LevelIndex)
    {
        LevelToLoad = LevelIndex;
        animator.SetTrigger("ChangeScene");
    }

    private void OnChangeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
