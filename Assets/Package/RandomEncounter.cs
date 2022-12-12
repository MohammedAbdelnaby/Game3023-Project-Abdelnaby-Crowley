using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public List<Enemy> listOfEnemies;
    public Enemy enemy;
    private TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        transitionManager = FindObjectOfType<TransitionManager>();
    }

    public void StartBattle()
    {
        int i = Random.Range(0, listOfEnemies.Count);
        enemy = listOfEnemies[i];
        transitionManager.FadeToLevel(3);
    }
}
