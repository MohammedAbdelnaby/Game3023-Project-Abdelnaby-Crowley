using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ability", menuName = "Ability/New Enemy Ability")]
public class EnemyAbility : ScriptableObject
{
    public string Name;
    public string Description;
    public AbilityType Type;
    public Buffs Buff;
    public Debuffs Debuff;
    public int ManaCost;
    public int Damage;
    public DamageType DamageType;
    public int ArmourGain;
    public int ArmourDamage;
    public int Heal;
}
