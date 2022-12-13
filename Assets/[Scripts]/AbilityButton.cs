using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    public PlayerAbility ability;

    public BattleSystem battleSystem;
    private Button button;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
        button = GetComponent<Button>();
        GetComponentInChildren<TMP_Text>().text = ability.name;
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = battleSystem.isPlayerTurn;
    }

    public void DoAbility()
    {
        if (battleSystem.isPlayerTurn)
        {
            if (ability.ManaCost <= battleSystem.playerMana)
            {
                OnClick();
                battleSystem.enemyArmour -= (battleSystem.enemyArmour > 0) ? ability.ArmourDamage : 0;
                if (battleSystem.enemyArmour > 0.0f)
                {
                    battleSystem.enemyArmour -= battleSystem.AbilityChange(ability);
                }
                else
                {
                    battleSystem.enemyHealth -= battleSystem.AbilityChange(ability);
                }
                battleSystem.enemyDebuff = ability.Debuff;

                battleSystem.playerMana -= ability.ManaCost;
                battleSystem.playerArmour += ability.ArmourGain;
                battleSystem.playerHealth += ability.Heal;
                battleSystem.playerBuff = ability.Buff;
                battleSystem.historyText.text = ability.Description;
                battleSystem.SwapTurns(); 
            }
            else
            {
                battleSystem.historyText.text = "You dont have enough mana for " + ability.name;
            }
        }
    }

    public void OnClick()
    {
        soundManager.PlaySoundFX(Sound.BUTTTON_CLICK, Channel.FX_BUTTON_CLICKED);
    }
}
