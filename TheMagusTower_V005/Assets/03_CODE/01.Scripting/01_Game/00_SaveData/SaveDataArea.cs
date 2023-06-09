using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataArea : MonoBehaviour
{
    public GameObject level;
    [SerializeField] GameManager gameManager;
    PlayerController playerData;
    [SerializeField] Items theRing, sword, rustedKey, silverKey, goldKey;
    [SerializeField] Enemy floorGuardian, EvilFireGazer, minotos, evilPhonix;
    [SerializeField] UI_Inventory_Item uiItemsSlots;
    [SerializeField] UIInventory uiInventory;
    [SerializeField] PlayerItemSlots itemsEquippedSlots;
    [SerializeField] ObjectTransitionUp levelEntrance, doorLevel;
    internal string[] uIInventoryfileDataPath;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerController>();
        //uIInventoryfileDataPath = new string[uiInventory.listofUIItems.Count];
    }
    private void GeneralSave()
    {
        gameManager.saveDataArea = this.gameObject.GetComponent<SaveDataArea>();
        // Save Player Data
        SaveManager.SavePlayerData(playerData);
        // Save Items Data
        SaveManager.SaveItemsData(theRing, "TheRingItemSavedData");
        SaveManager.SaveItemsData(rustedKey, "RustedKeyItemSavedData");
        SaveManager.SaveItemsData(silverKey, "SilverKeyItemSavedData");
        SaveManager.SaveItemsData(goldKey, "GoldKeyItemSavedData");
        SaveManager.SaveItemsData(sword, "SwordItemSavedData");
        // Save UI Inventory Data
        uIInventoryfileDataPath = new string[uiInventory.listofUIItems.Count];
        for (int n = 0; n < uiInventory.listofUIItems.Count; n++)
        {
            uiItemsSlots = uiInventory.listofUIItems[n].GetComponent<UI_Inventory_Item>();
            for (int i = 0; i < uiInventory.listofUIItems.Count; i++)
            {
                uIInventoryfileDataPath[i] = "dataSave" + i.ToString();
            }
            SaveManager.SaveuiInventoryData(uiItemsSlots, uiInventory, uIInventoryfileDataPath[n]);
        }
        // Save player equipped slots state
        SaveManager.SaveItemEquippedSlotsData(itemsEquippedSlots);
        // Save Level objects data
        SaveManager.SaveLevelObjectsData(levelEntrance, "entranceSavedData");
        SaveManager.SaveLevelObjectsData(doorLevel, "doorLevelSavedData");
        Debug.Log("Datos Guardados");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            gameManager.saveDataArea = this.gameObject.GetComponent<SaveDataArea>();

            GeneralSave();
        }
    }
}
