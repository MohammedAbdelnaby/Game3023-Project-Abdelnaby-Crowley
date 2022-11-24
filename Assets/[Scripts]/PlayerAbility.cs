using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/New Ability")]
public class PlayerAbility : ScriptableObject
{
    public string Name;
    public string Description;
    public AbilityType Type;
    public Buffs Buff;
    public Debuffs Debuff;
    public int ManaCost;
    public int Damage;
    public int Heal;
}
