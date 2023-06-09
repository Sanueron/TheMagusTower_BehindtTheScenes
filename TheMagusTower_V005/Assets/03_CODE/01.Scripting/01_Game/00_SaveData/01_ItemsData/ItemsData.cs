[System.Serializable]

public class ItemsData 
{
    public bool itemActiveState, rigidBodyKinematic, itemObtained, itemEquipped, itemPlacedInInventory;

    public ItemsData(Items itemsdata)
    {
        itemActiveState = itemsdata.gameObject.activeSelf;
        itemObtained = itemsdata.obtained;
        itemEquipped = itemsdata.equipped;
        itemPlacedInInventory = itemsdata.placedInInventory;

        // Save UI inventory state
    }

}
