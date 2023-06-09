using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIInventory uIInventory;
    UI_Inventory_Item uI_Inventory_Item;
    [SerializeField] internal SaveDataArea saveDataArea;
    PlayerController playerData;
    [SerializeField] Items theRingItemData, swordIData, rustedKeyIData, silverKeyIData, goldKeyIData;
    [SerializeField] Enemy floorGuardian, fireGazerEvilEye, minotos, evilPhonix;
    [SerializeField] GameObject player;
    [SerializeField] DamageArea fogDamageArea;
    [SerializeField] MovingPlatforms movingPlatform1, movingPlatform2, movingPlatform3;
    [SerializeField] ActionableObject lever;
    [SerializeField] PlayerItemSlots equippedSlots;
    [SerializeField] Menu_Manager menuManager;
    [SerializeField] CloseRangeAttack guardianFloorCloseRangeAttack;
    [SerializeField] GuardianF1 guardianF1;
    [SerializeField] ObjectTransitionUp levelEntrance, doorLevel;

    [SerializeField] Image spellUI;
    [SerializeField] Sprite defaultUIItemIconSprite, defaultSpellUI; 

    // UI Messages
    [SerializeField] GameObject deadMessage, reloadButton;

    private bool reload;
    // Start is called before the first frame update
    void Start()
    {
        deadMessage.SetActive(false);
        playerData = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (playerData.isDead)
        {
            player.SetActive(false);
            deadMessage.SetActive(true);
            ResetAllEnemies();
            fogDamageArea.playerInside = false;
            reloadButton.SetActive(true);
            //Corrutina para reactivar
            //StartCoroutine("ReStartFromCheckPoint", 3);
        }
        OpenMenu();
        OpenInventoryUI();
    }
    public void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuManager.menu.activeSelf)
            {
                menuManager.OpenMenu();
            }
            else if (menuManager.menu.activeSelf)
            {
                menuManager.CloseMenu();
            }
        }
    }
    public void OpenInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!menuManager.inventoryPanel.activeSelf)
            {
                menuManager.OpenInventory();
            }
            else if (menuManager.inventoryPanel.activeSelf)
            {
                menuManager.CloseInventory();
            }
        }
    }
    #region Resets
    public void ResetAllEnemies()
    {
        ResetEnemies(floorGuardian);
        if (guardianFloorCloseRangeAttack.closeRangeAttack.activeSelf)
        {
            guardianFloorCloseRangeAttack.closeRangeAttack.SetActive(false);
        }
        if(guardianF1.attacking == true)
        {
            guardianF1.attacking = false;
        }
        ResetEnemies(fireGazerEvilEye);
        ResetEnemies(minotos);
        ResetEnemies(evilPhonix);
    }
    public void ResetEnemies(Enemy enemy)
    {
        enemy.playerDetected = false;
        enemy.health = enemy.maxHealth;
        enemy.isDying = false;
        if(enemy.enemyAnimator != null)
        {
            enemy.enemyAnimator.SetBool("Dead", false);
        }
        enemy.rb.transform.position = enemy.initialPosition;
        enemy.rb.transform.rotation = enemy.initialRotation;
    }
    public void ResetLevelPlatforms()
    {
        lever.Reset();
        movingPlatform1.Reset();
        movingPlatform2.Reset();
        movingPlatform3.Reset();
    }
    #endregion
    public void ActivatePlayer()
    {
        player.SetActive(true);
        playerData.wait = false;
        playerData.speed = 20;
        deadMessage.SetActive(false);
        playerData.healingHP.Stop();
        playerData.healingMP.Stop();
    }
    #region Loads
    // Funciton to load Player Data
    public void LoadPlayerData()
    {
        PlayerData savedPlayerData = SaveManager.LoadPlayerData();
        playerData.isDead = false;
        playerData.earthShatter.Stop();
        playerData.maxHealth = savedPlayerData.maxHealth;
        playerData.health = savedPlayerData.health;
        playerData.maxMP = savedPlayerData.maxMp;
        playerData.magicPoints = savedPlayerData.mp;
        playerData.specialSpellUnlocked = savedPlayerData.specialSpellUnlockedSavedData;
        playerData.transform.position = new Vector3(savedPlayerData.position[0], savedPlayerData.position[1], savedPlayerData.position[2]);
        playerData.healingHP.Stop();
        playerData.healingMP.Stop();
        Debug.Log("Datos de jugador Cargados");
    }
    public void LoadEnemiesState()
    {
        EnemiesSavedData enemiesData = SaveManager.LoadEnenmyData(floorGuardian);
    }
    public void LoadItemsState()
    {
        // Repetir para cada objeto indican su ruta
        ItemsData theRingSavedData = SaveManager.LoadItemsdata("TheRingItemSavedData");
        theRingItemData.gameObject.SetActive(theRingSavedData.itemActiveState);
        theRingItemData.obtained = theRingSavedData.itemObtained;
        theRingItemData.equipped = theRingSavedData.itemEquipped;
        theRingItemData.placedInInventory = theRingSavedData.itemPlacedInInventory;
        // Actualizar inventario
        //
        ItemsData rustedKeySavedData = SaveManager.LoadItemsdata("RustedKeyItemSavedData");
        rustedKeyIData.gameObject.SetActive(rustedKeySavedData.itemActiveState);
        rustedKeyIData.obtained = rustedKeySavedData.itemObtained;
        rustedKeyIData.equipped = rustedKeySavedData.itemEquipped;
        rustedKeyIData.placedInInventory = rustedKeySavedData.itemPlacedInInventory;
        // Actualizar inventario
        //
        ItemsData silverKeySavedData = SaveManager.LoadItemsdata("SilverKeyItemSavedData");
        silverKeyIData.gameObject.SetActive(silverKeySavedData.itemActiveState);
        silverKeyIData.obtained = silverKeySavedData.itemObtained;
        silverKeyIData.equipped = silverKeySavedData.itemEquipped;
        silverKeyIData.placedInInventory = silverKeySavedData.itemPlacedInInventory;
        // Actualizar inventario
        //
        ItemsData goldKeySavedData = SaveManager.LoadItemsdata("GoldKeyItemSavedData");
        goldKeyIData.gameObject.SetActive(goldKeySavedData.itemActiveState);
        goldKeyIData.obtained = goldKeySavedData.itemObtained;
        goldKeyIData.equipped = goldKeySavedData.itemEquipped;
        goldKeyIData.placedInInventory = goldKeySavedData.itemPlacedInInventory;
        // Actualizar inventario
        //
        ItemsData swordSavedData = SaveManager.LoadItemsdata("SwordItemSavedData");
        swordIData.gameObject.SetActive(swordSavedData.itemActiveState);
        swordIData.obtained = swordSavedData.itemObtained;
        swordIData.equipped = swordSavedData.itemEquipped;
        swordIData.placedInInventory = swordSavedData.itemPlacedInInventory;
        // Actualizar inventario
        Debug.Log("Datos de objetos Cargados");


    }
    public void LoadPlayerInventory()
    {
        if(saveDataArea == null)
        {
            Debug.Log("No hay datos de inventario guardados");
        }
        else
        {
            for (int i = 0; i < uIInventory.listofUIItems.Count; i++)
            {
                UIInventoryData uIInventorySavedData = SaveManager.LoaduiInventoryData(saveDataArea.uIInventoryfileDataPath[i]);
                // Reasignar datos guardados
                uI_Inventory_Item = uIInventory.listofUIItems[i].GetComponent<UI_Inventory_Item>();
                uI_Inventory_Item.empty = uIInventorySavedData.uiSlotIsEmptySavedData[i];
                uI_Inventory_Item.uIItemTitle = uIInventorySavedData.uiTitleSavedData[i];
                uI_Inventory_Item.uIItemdescription = uIInventorySavedData.uiDescriptionSavedData[i];
                uI_Inventory_Item.uIItemskill = uIInventorySavedData.uiSkillSavedData[i];
            }
            Debug.Log("Datos de inventario Cargados");
        }
    }
    public void LoadInventoryUIIcons()
    {
        for (int i = 0; i < uIInventory.listofUIItems.Count; i++)
        {
            uI_Inventory_Item = uIInventory.listofUIItems[i].GetComponent<UI_Inventory_Item>();
            if (uI_Inventory_Item.empty == true)
            {
                uI_Inventory_Item.itemImage.sprite = defaultUIItemIconSprite;
            }
        }
    }
    public void LoadEquippedSlotsState()
    {
        PlayerSlotsSavedData equippedSlotsSavedData = SaveManager.LoadequippedLSlotsSavedData();
        equippedSlots.accessoryEquippedRightHand = equippedSlotsSavedData.accessoryEquippedRightHandSavedData;
        equippedSlots.accessoryEquippedLeftHand = equippedSlotsSavedData.accessoryEquippedLeftHandSavedData;
        equippedSlots.weaponEquipped = equippedSlotsSavedData.weaponEquippedSavedData;
        Debug.Log("Datos de objetos equipados Cargados");
    }
    public void LoadPlayerSpells()
    {
        if (!playerData.specialSpellUnlocked)
        {
            spellUI.sprite = defaultSpellUI;
        }
        Debug.Log("Datos de hechizos Cargados");
    }
    public void LoadLevelData()
    {
        LevelData entranceSavedData = SaveManager.LoadObjectsLevelData("entranceSavedData");
        levelEntrance.transform.position = new Vector3(entranceSavedData.posX, entranceSavedData.posY, entranceSavedData.posZ);
        //levelEntrance.transform.position = levelEntrance.initialPos;
        levelEntrance.controller = entranceSavedData.controller;
        levelEntrance.stopTimer = entranceSavedData.stopTimer;
        levelEntrance.move = true;
        LevelData doorSavedData = SaveManager.LoadObjectsLevelData("doorLevelSavedData");
        //doorLevel.transform.position = doorLevel.initialPos;
        doorLevel.transform.position = new Vector3(doorSavedData.posX, doorSavedData.posY, doorSavedData.posZ);
        doorLevel.controller = doorSavedData.controller;
        doorLevel.stopTimer = doorSavedData.stopTimer;
        doorLevel.move = doorSavedData.move;
        Debug.Log("Datos de nivel Cargados");
    }
    #endregion
}
