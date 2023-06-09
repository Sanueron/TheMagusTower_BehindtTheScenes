using Unity.VisualScripting;

[System.Serializable]

public class UIInventoryData
{
    public bool[] uiSlotIsEmptySavedData;
    public string[] uiTitleSavedData;
    public string[] uiDescriptionSavedData;
    public string[] uiSkillSavedData;

    public UIInventoryData(UI_Inventory_Item uiInventoryData, UIInventory uiInventory)
    {
        uiSlotIsEmptySavedData = new bool[uiInventory.listofUIItems.Count];
        uiTitleSavedData = new string[uiInventory.listofUIItems.Count];
        uiDescriptionSavedData = new string[uiInventory.listofUIItems.Count];
        uiSkillSavedData = new string[uiInventory.listofUIItems.Count];

        for (int i = 0; i < uiInventory.listofUIItems.Count; i++)
        {
            uiInventoryData = uiInventory.listofUIItems[i].GetComponent<UI_Inventory_Item>();
            uiSlotIsEmptySavedData[i] = uiInventoryData.empty;
            uiTitleSavedData[i] = uiInventoryData.uIItemTitle;
            uiDescriptionSavedData[i] = uiInventoryData.uIItemdescription;
            uiSkillSavedData[i] = uiInventoryData.uIItemskill;
        }
    }
}
