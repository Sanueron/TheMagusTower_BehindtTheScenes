using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Variable to access the ScriptableObject assigned
    [SerializeField] itemsScriptableObjects collectableItemObject;

    // Variable to access player stats
    [SerializeField] private PlayerController player;

    // Create variable to access player slots to equip items 
    [SerializeField] private PlayerItemSlots itemsToSlots;
    // and inventory
    [SerializeField] internal UIInventory uiInventory;
    private UI_Inventory_Item uiInventoryItem;

    // GameObject variables to activate UI panels and messages related with items
    [SerializeField] private GameObject messagePanel, messageText;

    // Rigidbody variable of the item
    internal Rigidbody itemRigidbody;
    internal Collider itemCollider;

    // Bool variables to reasign item identity variables
    // Type of item bool reasigned variables
    internal bool isAccessory, isWeapon, isCollectableItem;
    // Rarity of item bool reasigned variables
    internal bool common, rare, veryRare, ultraRare, legendary;
    // Effect of item bool reasigned variables
    internal bool none, healingHP, healingMP, increaseAttack;
    // Variable to indicate wether or not the item has been obtained by the player
    public bool obtained;
    internal bool equipped, placedInInventory = false;
    internal Animator animator;

    [SerializeField]
    [Tooltip("Where is the item at the beginning of the level")]
    internal GameObject itemInitialPos;

    // Local item variables
    private float coolDown;
    private int accessoryQuality, weaponQuality;
    private int itemStrenght, weaponStrenght;

    // UI item description variables
    internal Sprite itemSprite;
    internal string itemTitle, itemDescription, itemSkill;

    // Start is called before the first frame update
    void Start()
    {
        // Find player stats script
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        // Assign its components
        itemRigidbody = GetComponent<Rigidbody>();

        // Initialize its variables
        CheckUIDefVariables();
        CheckType();
        CheckRarity();
        AccessoryAttributes();
        WeaponAttributes();
        if (messagePanel.activeSelf)
        {
            messagePanel.SetActive(false);
            messageText.SetActive(false);
        }

        // Assign item to initial position
        gameObject.transform.position = itemInitialPos.transform.position;
    }

    private void Update()
    {
        if (!obtained)
        {
            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }
            gameObject.transform.SetParent(itemInitialPos.transform);
            gameObject.transform.position = itemInitialPos.transform.position;
            gameObject.transform.rotation = itemInitialPos.transform.rotation;
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = true;
            }
        }
        else
        {
            AddToInventory();
        }
        ItemEffect();
        AddToUIInventory();
    }

    #region Check ScriptableObject variables and compare to local variables methods
    private void CheckType()
    {
        switch (collectableItemObject.type)
        {
            case itemsScriptableObjects.Type.Accesory:
                isAccessory = true;
                isWeapon = false;
                isCollectableItem = false;
                accessoryQuality = (int)collectableItemObject.rarity + 1;
                break;

            case itemsScriptableObjects.Type.Weapon:
                isAccessory = false;
                isWeapon = true;
                isCollectableItem = false;
                weaponQuality = (int)collectableItemObject.rarity + 1;
                break;

            case itemsScriptableObjects.Type.CollectableItem:
                isAccessory = false;
                isWeapon = false;
                isCollectableItem = true;
                break;
        }
    }
    private void CheckRarity()
    {
        switch (collectableItemObject.rarity)
        {
            case itemsScriptableObjects.Rarity.Common:
                common = true;
                rare = false;
                veryRare = false;
                ultraRare = false;
                legendary = false;
                break;

            case itemsScriptableObjects.Rarity.Rare:
                common = false;
                rare = true;
                veryRare = false;
                ultraRare = false;
                legendary = false;
                break;

            case itemsScriptableObjects.Rarity.VeryRare:
                common = false;
                rare = false;
                veryRare = true;
                ultraRare = false;
                legendary = false;
                break;

            case itemsScriptableObjects.Rarity.UltraRare:
                common = false;
                rare = false;
                veryRare = false;
                ultraRare = true;
                legendary = false;
                break;

            case itemsScriptableObjects.Rarity.Legendary:
                common = false;
                rare = false;
                veryRare = false;
                ultraRare = false;
                legendary = true;
                break;
        }
    }
    private void CheckItemEffect()
    {
        switch (collectableItemObject.effect)
        {
            case itemsScriptableObjects.Effect.None:
                none = true;
                healingHP = false;
                healingMP = false;
                increaseAttack = false;
                break;

            case itemsScriptableObjects.Effect.HealingHP:
                none = false;
                healingHP = true;
                healingMP = false;
                increaseAttack = false;
                break;

            case itemsScriptableObjects.Effect.HealingMP:
                none = false;
                healingHP = false;
                healingMP = true;
                increaseAttack = false;
                break;

            case itemsScriptableObjects.Effect.IncreaseAttack:
                none = false;
                healingHP = false;
                healingMP = false;
                increaseAttack = true;
                break;
        }
    }
    private void CheckUIDefVariables()
    {
        itemSprite = collectableItemObject.itemIcon;
        itemTitle = collectableItemObject.itemName;
        itemDescription = collectableItemObject.description;
        itemSkill = collectableItemObject.skill;
    }
    #endregion

    private void AddToInventory()
    {
        if (obtained && !equipped)
        {
            if (isAccessory)
            {
                itemRigidbody.isKinematic = true;
                if (!itemsToSlots.accessoryEquippedRightHand)
                {
                    EquipItem(itemsToSlots.rightHandAccessoryPos);
                    itemsToSlots.accessoryEquippedRightHand = true;
                    equipped = true;
                    Debug.Log("Equipped in right hand");
                }
                else if (itemsToSlots.accessoryEquippedRightHand && !itemsToSlots.accessoryEquippedLeftHand)
                {
                    EquipItem(itemsToSlots.leftHandAccessoryPos);
                    itemsToSlots.accessoryEquippedLeftHand = true;
                    equipped = true;
                    Debug.Log("Equipped in left hand");
                }
                else
                {
                    Debug.Log("Can't equip more accessories, item stored in your bag");
                    gameObject.SetActive(false);
                }
            }
            else if (isWeapon)
            {
                if (!itemsToSlots.weaponEquipped)
                {
                    EquipItem(itemsToSlots.backWeaponPos);
                    itemsToSlots.weaponEquipped = true;
                    equipped = true;
                }
                else
                {
                    Debug.Log("Stored in bag");
                    gameObject.SetActive(false);
                }
            }
            else if (isCollectableItem)
            {
                itemRigidbody.isKinematic = true;
            }

        }
    }
    private void AddToUIInventory()
    {
        if (obtained)
        {
            for(int i = 0; i < uiInventory.listofUIItems.Count; i++)
            {
                if (!placedInInventory)
                {
                    uiInventoryItem = uiInventory.listofUIItems[i].GetComponent<UI_Inventory_Item>();
                    if (uiInventoryItem.empty)
                    {
                        uiInventoryItem.itemImage.sprite = itemSprite;
                        uiInventoryItem.uIItemTitle = itemTitle;
                        uiInventoryItem.uIItemdescription = itemDescription;
                        uiInventoryItem.uIItemskill = itemSkill;
                        placedInInventory = true;
                        uiInventoryItem.empty = false;
                    }
                }
            }
            /* If all slots are filled create new slot and assign
            if (!placedInInventory)
            {
                UI_Inventory_Item uiItem = Instantiate(uiItemPrefab, Vector3.zero, Quaternion.identity, contentPanel);
                //uiItem.transform.SetParent(contentPanel);
                listofUIItems.Add(uiItem);
            }
            */
        }
        if(isCollectableItem && placedInInventory)
        {
            if (obtained)
            {
                gameObject.SetActive(false);
            }
        }

    }

    internal void EquipItem(GameObject position)
    {
        transform.SetParent(position.transform);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
    }

    // Set Attributes methods
    internal void AccessoryAttributes()
    {
        var multiplier = 10;
        itemStrenght = multiplier * accessoryQuality;
    }
    internal void WeaponAttributes()
    {
        var multiplier = 10;
        weaponStrenght = multiplier * weaponQuality;
    }

    private void ItemEffect()
    {
        if (equipped)
        {
            float refreshSkill = 6;
            int effectAmountCorrection = 5;
            switch (collectableItemObject.effect)
            {
                case itemsScriptableObjects.Effect.None:
                    break;

                case itemsScriptableObjects.Effect.HealingHP:

                    coolDown += Time.deltaTime;
                    if (coolDown >= refreshSkill)
                    {
                        if (player.health < player.maxHealth)
                        {
                            Debug.Log("Heals HP");
                            player.health += itemStrenght / effectAmountCorrection;
                            coolDown = 0;
                        }
                    }
                    break;

                case itemsScriptableObjects.Effect.HealingMP:
                    break;

                case itemsScriptableObjects.Effect.IncreaseAttack:
                    break;
            }
        }
    }

    #region Trigger Detection
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            if (!obtained)
            {
                messagePanel.SetActive(true);
                messageText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.C))
                {
                    messagePanel.SetActive(false);
                    messageText.SetActive(false);
                    obtained = true;
                    AddToInventory();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            messagePanel.SetActive(false);
            messageText.SetActive(false);
        }
    }
    #endregion
}
