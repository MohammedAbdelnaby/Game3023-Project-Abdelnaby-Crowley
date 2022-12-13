using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GetComponent<SoundManager>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                break;
            case "Credits":
                break;
            case "OverWorld":
                soundManager.PlaySoundFX(Sound.OVERWORLD, Channel.MUSIC);
                break;
            case "BattleScene":
                soundManager.PlaySoundFX(Sound.BATTLE, Channel.MUSIC);
                break;
            default:
                break;
        }
    }

    public void OnClick()
    {
        soundManager.PlaySoundFX(Sound.BUTTTON_CLICK, Channel.FX_BUTTON_CLICKED);
    }
}
