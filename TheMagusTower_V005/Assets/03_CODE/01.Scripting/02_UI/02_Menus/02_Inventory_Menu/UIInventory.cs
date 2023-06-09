using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UI_Inventory_Item uiItemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] internal GameObject inventoryDescriptionPanel;
    [SerializeField] internal UIInventoryDescription uIInventoryDescription;

    // Description window variables - Modify each time in UI_Inventory_Item script
    public Sprite selectedItemSprite;
    public string title, description, skill;

    internal List<UI_Inventory_Item> listofUIItems = new List<UI_Inventory_Item>();

    private void Awake()
    {
        HideDescription();
    }
    internal void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            UI_Inventory_Item uiItem = Instantiate(uiItemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
            //uiItem.transform.SetParent(contentPanel);
            listofUIItems.Add(uiItem);
        }
    }
    public void HideDescription()
    {
        inventoryDescriptionPanel.SetActive(false);
    }
    public void ShowDescription()
    {
        uIInventoryDescription.SetDescriptionWindow(selectedItemSprite, title, description, skill);
        inventoryDescriptionPanel.SetActive(true);
    }
}
