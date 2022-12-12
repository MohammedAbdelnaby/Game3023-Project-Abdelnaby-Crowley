using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy/New enemy")]
public class Enemy : ScriptableObject
{
    public Sprite battleSprite;
    public string enemyName;
    public int health;
    public int mana;
    public AudioClip battleTheme;
    public List<EnemyAbility> abilities;
    public EnemyBehaviour behaviour;
}
