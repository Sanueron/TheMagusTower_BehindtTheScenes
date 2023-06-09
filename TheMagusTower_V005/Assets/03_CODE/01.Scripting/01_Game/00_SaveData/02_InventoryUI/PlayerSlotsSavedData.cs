[System.Serializable]

public class PlayerSlotsSavedData  
{
    public bool accessoryEquippedRightHandSavedData, accessoryEquippedLeftHandSavedData, weaponEquippedSavedData;

    public PlayerSlotsSavedData(PlayerItemSlots itemsSlots)
    {
        accessoryEquippedRightHandSavedData = itemsSlots.accessoryEquippedRightHand;
        accessoryEquippedLeftHandSavedData = itemsSlots.accessoryEquippedLeftHand;
        weaponEquippedSavedData = itemsSlots.weaponEquipped;
    }
}
