using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager 
{
    #region Save & Load Player Data
    // Function to save data from PlayerData in a binary format in local device
    public static void SavePlayerData(PlayerController playerData)
    {
        PlayerData savedData = new PlayerData(playerData);
        string dataPath = Application.persistentDataPath + "savedData";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }
    // Function + method PlayerData to Load the saved Player data
    public static PlayerData LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "savedData";

        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData savedData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            return null;
        }
    }
    #endregion
    #region Save & Load Enemies Data
    public static void SaveEnemiesData(Enemy enemyData)
    {
        EnemiesSavedData enemySavedData = new EnemiesSavedData(enemyData);
        string dataPath = Application.persistentDataPath + "enemySaveData";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, enemySavedData);
        fileStream.Close();
    }
    public static EnemiesSavedData LoadEnenmyData(Enemy enemy)
    {
        string dataPath = Application.persistentDataPath + "enemySaveData";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            EnemiesSavedData enemiesData = (EnemiesSavedData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return enemiesData;
        }
        else
        {
            return null;
        }
    }
    #endregion
    #region Save & Load Items data
    public static void SaveItemsData(Items itemsData, string fileDataPath)
    {
        ItemsData savedData = new ItemsData(itemsData);
        string dataPath = Application.persistentDataPath + fileDataPath;
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }
    public static ItemsData LoadItemsdata(string fileDataPath)
    {
        string dataPath = Application.persistentDataPath + fileDataPath;
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            ItemsData savedData = (ItemsData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            return null;
        }
    }
    #endregion
    #region InventoryData
    public static void SaveuiInventoryData(UI_Inventory_Item uiInventoryData, UIInventory uiInventory, string fileDataPath)
    {
        UIInventoryData savedData = new UIInventoryData(uiInventoryData, uiInventory);
        string dataPath = Application.persistentDataPath + fileDataPath;
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }
    // Function + method PlayerData to Load the saved Player data
    public static UIInventoryData LoaduiInventoryData(string fileDataPath)
    {
        string dataPath = Application.persistentDataPath + fileDataPath;
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            UIInventoryData savedData = (UIInventoryData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            return null;
        }
    }

    public static void SaveItemEquippedSlotsData(PlayerItemSlots equippedSlotsData)
    {
        PlayerSlotsSavedData savedData = new PlayerSlotsSavedData(equippedSlotsData);
        string dataPath = Application.persistentDataPath + "equippedSlotsSavedData";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }
    public static PlayerSlotsSavedData LoadequippedLSlotsSavedData()
    {
        string dataPath = Application.persistentDataPath + "equippedSlotsSavedData";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerSlotsSavedData savedData = (PlayerSlotsSavedData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            return null;
        }
    }
    #endregion
    public static void SaveLevelObjectsData(ObjectTransitionUp levelOjectsData, string fileName)
    {
        LevelData savedData = new LevelData(levelOjectsData);
        string dataPath = Application.persistentDataPath + fileName;
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }
    public static LevelData LoadObjectsLevelData(string fileName)
    {
        string dataPath = Application.persistentDataPath + fileName;

        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            LevelData savedData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            return null;
        }
    }
}
