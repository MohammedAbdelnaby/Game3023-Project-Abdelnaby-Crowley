using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator bTransiton;

    public Animator lTransition;

    private RandomEncounter randomEncounterManager;

    private void Awake()
    {
        randomEncounterManager = FindObjectOfType<RandomEncounter>();

    }

    public void BattleTransition()
    {
        StartCoroutine(PlayBattleTransiton());
    }

    public IEnumerator PlayBattleTransiton()
    {

        bTransiton.SetTrigger("SpinStart");

        yield return new WaitForSeconds(3.0f);

        randomEncounterManager.StartBattle();

    }

    public IEnumerator PlayLevelTransition()
    {
        lTransition.SetTrigger("StartFade");

        yield return new WaitForSeconds(0.84f);

    }
}
