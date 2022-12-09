using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class RandomEncounter : MonoBehaviour
{
    

    public List<Enemy> listOfEnemies;
    
    public Enemy enemy;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void StartBattle()
    {
        int i = Random.Range((int)0, (int)listOfEnemies.Count);

        enemy = listOfEnemies[i];

        SceneManager.LoadScene(4);
    }

    //public IEnumerator PlayBattleTransiton()
    //{
        
    //    transiton.SetTrigger("SpinStart");

    //    yield return new WaitForSeconds(1.0f);

    //}
}
