using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [Header("Battle UI")]
    public GameObject buttonPrefab;
    public GameObject mainMenu;
    public GameObject attackParent;
    public GameObject magicParent;
    public GameObject defenceParent;
    public GameObject ItemParent;

    private BattleSystem battleSystem;
    private List<PlayerAbility> playerAttackAbilies;
    private List<PlayerAbility> playerMagicAbilies;
    private List<PlayerAbility> playerDefenceAbilies;
    private int mainMenuState;

    public int MainMenuState { get => mainMenuState; set => mainMenuState = (value < 0) ? 0 : value; }

    // Start is called before the first frame update
    void Start()
    {
        MainMenuState = 0;
        battleSystem = GameObject.Find("BattleManager").GetComponent<BattleSystem>();
        playerAttackAbilies = new List<PlayerAbility>();
        playerMagicAbilies = new List<PlayerAbility>();
        playerDefenceAbilies = new List<PlayerAbility>();
        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        UpdateUI();
    }

    private void ChangeState()
    {
        switch (mainMenuState)
        {
            case 0://Main Menu
                mainMenu.SetActive(true);
                attackParent.SetActive(false);
                magicParent.SetActive(false);
                defenceParent.SetActive(false);
                ItemParent.SetActive(false);
                break;
            case 1: //Attack
                mainMenu.SetActive(false);
                attackParent.SetActive(true);
                magicParent.SetActive(false);
                defenceParent.SetActive(false);
                ItemParent.SetActive(false);
                break;
            case 2:// Magic
                mainMenu.SetActive(false);
                attackParent.SetActive(false);
                magicParent.SetActive(true);
                defenceParent.SetActive(false);
                ItemParent.SetActive(false);
                break;
            case 3:// Defence
                mainMenu.SetActive(false);
                attackParent.SetActive(false);
                magicParent.SetActive(false);
                defenceParent.SetActive(true);
                ItemParent.SetActive(false);
                break;
            case 4:// Item
                mainMenu.SetActive(false);
                attackParent.SetActive(false);
                magicParent.SetActive(false);
                defenceParent.SetActive(false);
                ItemParent.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void UpdateUI()
    {
        //Enemy UI, and player UI (health, mana)
    }

    private void CreateButtons()
    {
        foreach (PlayerAbility ability in battleSystem.playerAbilities)
        {
            GameObject goTemp = buttonPrefab;
            goTemp.GetComponent<AbilityButton>().ability = ability;
            switch (ability.Type)
            {
                case AbilityType.NONE:
                    break;
                case AbilityType.ATTACK:
                    GameObject.Instantiate(goTemp, Vector3.zero, Quaternion.identity, attackParent.transform);
                    playerAttackAbilies.Add(ability);
                    break;
                case AbilityType.MAGIC:
                    GameObject.Instantiate(goTemp, Vector3.zero, Quaternion.identity, magicParent.transform);
                    playerMagicAbilies.Add(ability);
                    break;
                case AbilityType.DEFENCE:
                    GameObject.Instantiate(goTemp, Vector3.zero, Quaternion.identity, defenceParent.transform);
                    playerDefenceAbilies.Add(ability);
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeStateButton(int value)
    {
        MainMenuState = value;
    }
}
