using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyTile : MonoBehaviour
{
    public RandomEncounter randomEncounterManager;

    public TransitionManager transitionManager;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Random.Range((int)0, (int)1) < 1)
            {
                Debug.Log("You are under attack");
                transitionManager.BattleTransition();

                
            }

        }

    }

    //IEnumerator LoadBattle()
    //{
    //    yingYangBall.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
    //    transiton.SetTrigger("SpinStart");

    //    yield return new WaitForSeconds(1.0f);

    //    transiton.SetTrigger("FadeStart");

    //    yield return new WaitForSeconds(1.0f);

    //    randomEncounterManager.StartBattle();


    //}
}
