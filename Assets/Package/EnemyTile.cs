using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTile : MonoBehaviour
{
    public RandomEncounter randomEncounterManager;
    public TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        transitionManager = FindObjectOfType<TransitionManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Random.value >= 0.5f)
            {
                randomEncounterManager.StartBattle();
                collision.GetComponent<PlayerMovement>().Speed = 0.0f;
                transitionManager.FadeToLevel(4);
                Debug.Log("You are under attack");
            }
        }

    }
}
