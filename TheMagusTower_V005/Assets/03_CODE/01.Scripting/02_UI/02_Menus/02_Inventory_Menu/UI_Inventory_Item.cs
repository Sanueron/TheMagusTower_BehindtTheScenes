using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class UI_Inventory_Item : MonoBehaviour
{
    [SerializeField] private UIInventory uIInventory;
    [SerializeField] internal Image itemImage;
    [SerializeField] internal Sprite defaultEmptySlotImage;
    [SerializeField] internal string uIItemTitle, uIItemdescription, uIItemskill;
    internal string uIItemDefaultTitle = "Title", uIItemDefaultDescription = "Description", uIItemDefaultSkill = "Skill";

    //public event Action<UI_Inventory_Item> OnItemClicked, OnItemDragged;

    internal bool empty = true;

    private void Awake()
    {
        uIInventory = GetComponentInParent<UIInventory>();
        //ResetData();
        //Deselect();
    }
    
    private void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }
    private void Deselect()
    {
        // borderImage.SetActive(false);
    }
    private void SetData(Sprite sprite)
    {
        //this.itemImage.gameObject.SetActive(false);
        this.itemImage.sprite = sprite;
        empty = false;
    }
    public void ShowItemDescription()
    {
        // Pass Description window new values
        GetItemValues();

        uIInventory.ShowDescription();
    }
    internal void GetItemValues()
    {
        uIInventory.selectedItemSprite = itemImage.sprite;
        uIInventory.title = uIItemTitle;
        uIInventory.description = uIItemdescription;
        uIInventory.skill = uIItemskill;
    } 
}
