using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [SerializeField]
    private int count = 0;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            UpdateGraphic();
        }
    }

    [SerializeField]
    Image itemIcon;



    [SerializeField]
    TextMeshProUGUI itemCountText;


    [SerializeField]
    private PlayerMovement playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<PlayerMovement>();
        UpdateGraphic();
    }

    //Change Icon and count
    void UpdateGraphic()
    {
        if (count < 1)
        {
            item = null;
            itemIcon.gameObject.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        }
        else
        {
            //set sprite to the one from the item
            itemIcon.sprite = item.icon;
            //spriteIcon.sprite = item.icon;
            itemIcon.gameObject.SetActive(true);
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = count.ToString();
        }
    }

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            item.Use();
            if (item.typeOfItem == ItemType.RESTOREHP)
            {
                playerRef.currentHP += item.restoreValue;
                if (playerRef.currentHP > playerRef.maxHP)
                {
                    playerRef.currentHP = playerRef.maxHP;
                    
                }
                Debug.Log(playerRef.currentHP);
            }
            else if (item.typeOfItem == ItemType.RESTOREMP)
            {
                playerRef.currentMP += item.restoreValue;
                if (playerRef.currentMP > playerRef.maxMP)
                {
                    playerRef.currentMP = playerRef.maxMP;
                    
                }
                Debug.Log(playerRef.currentMP);
            }
            Count--;
            

        }
    }

    private bool CanUseItem()
    {
        return (item != null && count > 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = item.description;
            nameText.text = item.name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }

    
}
