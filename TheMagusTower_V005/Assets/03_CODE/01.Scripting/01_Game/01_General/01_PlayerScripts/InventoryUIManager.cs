using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private UIInventory uI_Inventory;

    [SerializeField] private int inventorySize;

    // Start is called before the first frame update
    void Start()
    {
        uI_Inventory.InitializeInventoryUI(inventorySize);
    }

}
