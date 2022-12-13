using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string description = "";
    public int restoreValue;
    public int damageValue;
    public ItemType typeOfItem;

    

    public void Use()
    {


            Debug.Log("Used item: " + name + " - " + description);
        
        
    }
}
