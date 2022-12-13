using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTile : MonoBehaviour
{
    public RandomEncounter randomEncounterManager;
    public TransitionManager transitionManager;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        transitionManager = FindObjectOfType<TransitionManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (collision.tag == "Player")
            {
                if (Random.value >= 0.90f)
                {
                    soundManager.PlaySoundFX(Sound.BATTLE_ALERT, Channel.FX_BATTLE_ALERT);
                    randomEncounterManager.StartBattle();
                    collision.GetComponent<PlayerMovement>().Speed = 0.0f;
                    PlayerPrefs.SetFloat("X", collision.transform.position.x);
                    PlayerPrefs.SetFloat("Y", collision.transform.position.y);
                    PlayerPrefs.SetFloat("Z", collision.transform.position.z);
                    transitionManager.FadeToLevel(3);
                    Debug.Log("You are under attack");
                }
            } 
        }
    }
}
