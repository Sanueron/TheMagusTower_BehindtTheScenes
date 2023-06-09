[System.Serializable]

public class PlayerData
{
    // Define variables to Save
    public int maxHealth, health, maxMp, mp;
    public float[] position = new float[3];
    public bool specialSpellUnlockedSavedData;

    // Create method of the class to assign values to variables to Save
    public PlayerData(PlayerController playerData)
    {
        maxHealth = playerData.maxHealth;
        health = playerData.health;
        maxMp = playerData.maxMP;
        mp = playerData.magicPoints;
        specialSpellUnlockedSavedData = playerData.specialSpellUnlocked;
        position[0] = playerData.transform.position.x;
        position[1] = playerData.transform.position.y;
        position[2] = playerData.transform.position.z;
    }
}
