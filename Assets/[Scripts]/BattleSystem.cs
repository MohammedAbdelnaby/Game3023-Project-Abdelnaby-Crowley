using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [Header("Battle UI")]
    public TMP_Text historyText;

    [Header("Player Properties")]
    public int playerHealth;
    public int playerMana;
    public string playerName;
    public PlayerAbility[] playerAbilities;
    public Buffs playerBuff;
    public Debuffs playerDebuff;
    public bool isPlayerTurn = true;

    [Header("Enemy Properties")]
    public string enemyName;
    public int enemyHealth;
    public int enemyMana;
    public List<PlayerAbility> enemyAbilities;
    public Buffs enemyBuff;
    public Debuffs enemyDebuff;

    float timeBetweenTurns = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        historyText.text = enemyName + " stands in your way";
        playerAbilities = Resources.LoadAll<PlayerAbility>("PlayerAbilities");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerTurn)
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
        if (isPlayerTurn)
        {
            historyText.text = playerName + " strikes " + enemyName;
            SwapTurns();
        }
        
    }

    public void Magic()
    {
        if (isPlayerTurn)
        {
            historyText.text = playerName + " Casts Flame Wheel";
            playerMana -= 10;
            SwapTurns();
        }
        
    }

    public void UseItem()
    {
        if (isPlayerTurn)
        {
            historyText.text = "You have no items";
        }
        
    }

    public void RunAway()
    {
        if (isPlayerTurn)
        {
            historyText.text = "You can't run away";
        }
        
    }

    public void Defend()
    {
        if (isPlayerTurn)
        {
            historyText.text = playerName + " braces for impact";
            SwapTurns();
        }
        
    }

    public void SwapTurns()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    public void EnemyAction()
    {
        historyText.text = enemyName + " does nothing";
        timeBetweenTurns = 0.0f;
        SwapTurns();
    }
}
