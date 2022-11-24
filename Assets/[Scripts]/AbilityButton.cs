using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    public PlayerAbility ability;

    private BattleSystem battleSystem;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
        button = GetComponent<Button>();
        GetComponentInChildren<TMP_Text>().text = ability.name;
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = battleSystem.isPlayerTurn;
    }

    public void DoAbility()
    {
        battleSystem.enemyHealth -= ability.Damage;
        battleSystem.playerMana -= ability.ManaCost;
        battleSystem.enemyDebuff = ability.Debuff;
        battleSystem.playerBuff = ability.Buff;
        battleSystem.playerHealth += ability.Heal;
    }
}
