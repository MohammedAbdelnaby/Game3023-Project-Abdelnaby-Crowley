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
    public int playerArmour;
    public PlayerAbility[] playerAbilities;
    public Buffs playerBuff;
    public Debuffs playerDebuff;
    public bool isPlayerTurn = true;

    [Header("Enemy Properties")]
    public Enemy enemy;
    public int enemyHealth;
    public int enemyMana;
    public int enemyArmour;
    public Buffs enemyBuff;
    public Debuffs enemyDebuff;
    public SpriteRenderer enemySprite;
    public float timeBetweenTurns = 2.0f;

    public List<EnemyAbility> AttackAbilities;
    public List<EnemyAbility> MagicAbilities;
    public List<EnemyAbility> DefenceAbilities;

    public int EnemyTurnCount;

    private TransitionManager transitionManager;

    [SerializeField]
    private PlayerMovement playerRef;

    private void Awake()
    {
        enemy = FindObjectOfType<RandomEncounter>().enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadSave();
        enemySprite.sprite = enemy.battleSprite;
        historyText.text = enemy.enemyName + " stands in your way";
        playerAbilities = Resources.LoadAll<PlayerAbility>("PlayerAbilities");
        SplitEnemyAbilities();
        Initialize();
        transitionManager = FindObjectOfType<TransitionManager>();
    }

    private void Update()
    {
        if (enemyArmour < 0)
        {
            enemyArmour = 0;
        }
        if (playerArmour < 0)
        {
            playerArmour = 0;
        }
        if (enemyHealth <= 0.0f)
        {
            Save();
            transitionManager.FadeToLevel(2);
        }
        if (playerHealth <= 0.0f)
        {
            ResetSave();
            PlayerPrefs.SetFloat("X", 0.0f);
            PlayerPrefs.SetFloat("Y", 1.0f);
            PlayerPrefs.SetFloat("Z", 0.0f);
        }
    }


    public void SwapTurns()
    {
        isPlayerTurn = !isPlayerTurn;
        StateUpdate();
        if (!isPlayerTurn)
        {
            StartCoroutine(EnemyAction());
            EnemyTurnCount++;
        }
    }

    public IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(timeBetweenTurns);
        switch (enemy.behaviour)
        {
            case EnemyBehaviour.DEFENSIVE:
                Defence();
                break;
            case EnemyBehaviour.AGGRESSIVE:
                Agressive();
                break;
            case EnemyBehaviour.HARMLESS:
                Harmless();
                break;
            case EnemyBehaviour.SCARED:
                Scared();
                break;
            default:
                break;
        }
        SwapTurns();
    }



    private void Initialize()
    {
        enemyHealth = enemy.health;
        enemyMana = enemy.mana;
    }

    private void SplitEnemyAbilities()
    {
        for (int i = 0; i < enemy.abilities.Count; i++)
        {
            switch (enemy.abilities[i].Type)
            {
                case AbilityType.NONE:
                    break;
                case AbilityType.ATTACK:
                    AttackAbilities.Add(enemy.abilities[i]);
                    break;
                case AbilityType.MAGIC:
                    MagicAbilities.Add(enemy.abilities[i]);
                    break;
                case AbilityType.DEFENCE:
                    DefenceAbilities.Add(enemy.abilities[i]);
                    break;
                default:
                    break;
            }
        }
    }

    private void Defence()
    {
        if (EnemyTurnCount > 4)
        {
            EnemyTurnCount = 1;
        }
        if (enemyHealth/enemy.health > 0.10f)
        {
            UseAbility(RandomAbility(DefenceAbilities));
            return;
        }
        switch (EnemyTurnCount)
        {
            case 1:
                UseAbility(RandomAbility(DefenceAbilities));
                break;
            case 2:
                UseAbility(RandomAbility((Random.value > 0.5) ? MagicAbilities : AttackAbilities));
                break;
            case 3:
                if (enemyArmour > playerArmour)
                {
                    UseAbility(RandomAbility((Random.value > 0.5) ? MagicAbilities : AttackAbilities));
                }
                else
                {
                    UseAbility(RandomAbility(DefenceAbilities));
                }
                break;
            case 4:
                UseAbility(RandomAbility(AttackAbilities));
                break;
            default:
                break;
        }
    }

    private void Agressive()
    {
        if (EnemyTurnCount > 4)
        {
            EnemyTurnCount = 1;
        }
        if (enemyHealth / enemy.health > 0.10f)
        {
            UseAbility(RandomAbility(AttackAbilities));
            return;
        }
        switch (EnemyTurnCount)
        {
            case 1:
                UseAbility(RandomAbility(AttackAbilities));
                break;
            case 2:
                UseAbility(RandomAbility((Random.value > 0.5) ? MagicAbilities : AttackAbilities));
                break;
            case 3:
                if (enemyHealth / enemy.health > 0.50f)
                {
                    UseAbility(RandomAbility(DefenceAbilities));
                }
                else
                {
                    UseAbility(RandomAbility(MagicAbilities));
                }
                break;
            case 4:
                UseAbility(RandomAbility(AttackAbilities));
                break;
            default:
                break;
        }
    }

    private void Harmless()
    {
        if (enemyHealth /enemy.health < 0.4)
        {
            EnemyFlee();
        }
        UseAbility(RandomAbility((Random.value > 0.5f) ? MagicAbilities : DefenceAbilities));
    }

    private void Scared()
    {
        if (EnemyTurnCount > 5)
        {
            EnemyTurnCount = 1;
        }
        if (enemyHealth / enemy.health > 0.10f)
        {
            EnemyFlee();
            return;
        }
        switch (EnemyTurnCount)
        {
            case 1:
                EnemyFlee();
                break;
            case 2:
                UseAbility(RandomAbility(DefenceAbilities));
                break;
            case 3:
                UseAbility(RandomAbility(AttackAbilities));
                break;
            case 4:
                UseAbility(RandomAbility(MagicAbilities));
                break;
            case 5:
                if (playerHealth < 50)
                {
                    EnemyFlee();
                }
                break;
            default:
                break;
        }
    }

    //https://youtu.be/LDgQ-spnrYY Helped with Lambda Expression
    private EnemyAbility RandomAbility(List<EnemyAbility> abilities)
    {
        EnemyAbility temp = abilities[Random.Range(0, abilities.Count)];
        return (temp.ManaCost > enemyMana) ? abilities.Find((EnemyAbility x) => x.ManaCost <= enemyMana) : temp;
    }

    private void UseAbility(EnemyAbility ability)
    {
        if (ability != null)
        {
            //player stats
            playerArmour -= (playerArmour > 0) ? ability.ArmourDamage : 0;
            if (playerArmour > 0.0f)
            {
                playerArmour -= AbilityChange(ability);
            }
            else
            {
                playerHealth -= AbilityChange(ability);
            }
            playerDebuff = ability.Debuff;

            //Enemy stats
            enemyMana -= ability.ManaCost;
            enemyArmour += ability.ArmourGain;
            enemyHealth += ability.Heal;
            enemyBuff = ability.Buff;
            historyText.text = enemy.enemyName + ": " + ability.Description; 
        }
        else
        {
            historyText.text = enemy.enemyName + ": " + "Passed the turn";
        }
    }

    private int AbilityChange(EnemyAbility ability)
    {
        int temp = ability.Damage;
        switch (enemyBuff)
        {
            case Buffs.NONE:
                break;
            case Buffs.STRENGHT:
                temp += (int)(ability.Damage / 2);
                enemyBuff = Buffs.NONE;
                break;
            default:
                break;
        }

        switch (playerBuff)
        {
            case Buffs.NONE:
                break;
            case Buffs.ICE_RESISTANCE:
                if(ability.DamageType == DamageType.ICE)
                    temp = (int)(temp / 2);
                playerBuff = Buffs.NONE;
                break;
            case Buffs.FIRE__RESISTANCE:
                if (ability.DamageType == DamageType.FIRE)
                    temp = (int)(temp / 2);
                playerBuff = Buffs.NONE;
                break;
            case Buffs.SLASHING__RESISTANCE:
                if (ability.DamageType == DamageType.SLASHING)
                    temp = (int)(temp / 2);
                playerBuff = Buffs.NONE;
                break;
            case Buffs.ACID__RESISTANCE:
                if (ability.DamageType == DamageType.ACID)
                    temp = (int)(temp / 2);
                playerBuff = Buffs.NONE;
                break;
            default:
                break;
        }

        switch (enemyDebuff)
        {
            case Debuffs.WEAKNESS:
                temp = (int)(temp / 2);
                enemyDebuff = Debuffs.NONE;
                break;
            default:
                break;
        }
        return temp;
    }

    private void StateUpdate()
    {
        //player Debuff
        switch (playerDebuff)
        {
            case Debuffs.NONE:
                break;
            case Debuffs.SLOW:
                playerArmour = (int)(playerArmour / 2);
                playerDebuff = Debuffs.NONE;
                break;
            case Debuffs.STUN:
                isPlayerTurn = false;
                historyText.text = "You are stunned";
                playerDebuff = Debuffs.NONE;
                break;
            default:
                break;
        }

        switch (enemyDebuff)
        {
            case Debuffs.NONE:
                break;
            case Debuffs.SLOW:
                enemyArmour = (int)(enemyArmour / 2);
                playerDebuff = Debuffs.NONE;
                break;
            case Debuffs.STUN:
                isPlayerTurn = true;
                historyText.text = enemy.name +" is stunned";
                enemyDebuff = Debuffs.NONE;
                break;
            default:
                break;
        }
    }

    public int AbilityChange(PlayerAbility ability)
    {
        int temp = ability.Damage;
        switch (playerBuff)
        {
            case Buffs.NONE:
                break;
            case Buffs.STRENGHT:
                temp += (int)(ability.Damage / 2);
                playerBuff = Buffs.NONE;
                break;
            default:
                break;
        }

        switch (enemyBuff)
        {
            case Buffs.NONE:
                break;
            case Buffs.ICE_RESISTANCE:
                if (ability.DamageType == DamageType.ICE)
                    temp = (int)(temp / 2);
                enemyBuff = Buffs.NONE;
                break;
            case Buffs.FIRE__RESISTANCE:
                if (ability.DamageType == DamageType.FIRE)
                    temp = (int)(temp / 2);
                enemyBuff = Buffs.NONE;
                break;
            case Buffs.SLASHING__RESISTANCE:
                if (ability.DamageType == DamageType.SLASHING)
                    temp = (int)(temp / 2);
                enemyBuff = Buffs.NONE;
                break;
            case Buffs.ACID__RESISTANCE:
                if (ability.DamageType == DamageType.ACID)
                    temp = (int)(temp / 2);
                enemyBuff = Buffs.NONE;
                break;
            default:
                break;
        }

        switch (playerDebuff)
        {
            case Debuffs.WEAKNESS:
                temp = (int)(temp / 2);
                playerDebuff = Debuffs.NONE;
                break;
            default:
                break;
        }
        return temp;
    }

    private void EnemyFlee()
    {
        if (Random.value > playerHealth / 100)
        {
            transitionManager.FadeToLevel(2);
        }
        else
        {
            historyText.text = enemy.name + " tried to flee but failed.";
        }
    }

    public void PlayerFlee()
    {
        if (Random.value > (enemyHealth / enemy.health))
        {
            transitionManager.FadeToLevel(2);
        }
        else
        {
            historyText.text = "You tried to flee but failed.";
            SwapTurns();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Health", playerHealth);
        PlayerPrefs.SetInt("Mana", playerMana);
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("Health", 100);
        PlayerPrefs.SetInt("Mana", 100);
    }

    public void LoadSave()
    {
        playerHealth = PlayerPrefs.GetInt("Health");
        playerMana = PlayerPrefs.GetInt("Mana");
    }
}
