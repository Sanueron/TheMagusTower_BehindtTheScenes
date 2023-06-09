[System.Serializable]

public class EnemiesSavedData
{
    // Define variables to Save
    public int health;
    public float[] position = new float[3];
    public bool playerDetected;

    // Create method of the class to assign values to variables to Save
    public EnemiesSavedData(Enemy enemyData)
    {
        if (enemyData.isDying)
        {
            health = enemyData.minHealth;
        }
        else
        {
            health = enemyData.maxHealth;
        }
        playerDetected = false;
        position[0] = enemyData.transform.position.x;
        position[1] = enemyData.transform.position.y;
        position[2] = enemyData.transform.position.z;
    }
}
