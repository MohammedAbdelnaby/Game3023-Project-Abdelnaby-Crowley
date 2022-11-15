using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Text historyText;

    public int playerHealth;
    public int playerMana;
    public string enemyName;
    public string playerName;
    public Text healthText;
    public Text manaText;
    public Text playerNameText;

    private bool playerTurn = true;

    float timeBetweenTurns = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        playerName = playerNameText.text.Replace("\n", "");
        historyText.text = enemyName + " stands in your way";
        healthText.text = playerHealth.ToString();
        manaText.text = playerMana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn)
        {
            timeBetweenTurns += Time.deltaTime;
            if (timeBetweenTurns >= 3.0f)
            {
                EnemyAction();
            }
            
        }
    }

    public void Attack()
    {
        if (playerTurn)
        {
            historyText.text = playerName + " strikes " + enemyName;
            SwapTurns();
        }
        
    }

    public void Magic()
    {
        if (playerTurn)
        {
            historyText.text = playerName + " Casts Flame Wheel";
            playerMana -= 10;
            manaText.text = playerMana.ToString();
            SwapTurns();
        }
        
    }

    public void UseItem()
    {
        if (playerTurn)
        {
            historyText.text = "You have no items";
        }
        
    }

    public void RunAway()
    {
        if (playerTurn)
        {
            historyText.text = "You can't run away";
        }
        
    }

    public void Defend()
    {
        if (playerTurn)
        {
            historyText.text = playerName + " braces for impact";
            SwapTurns();
        }
        
    }

    public void SwapTurns()
    {
        playerTurn = !playerTurn;
    }

    public void EnemyAction()
    {
        historyText.text = enemyName + " does nothing";
        timeBetweenTurns = 0.0f;
        SwapTurns();
    }
}
