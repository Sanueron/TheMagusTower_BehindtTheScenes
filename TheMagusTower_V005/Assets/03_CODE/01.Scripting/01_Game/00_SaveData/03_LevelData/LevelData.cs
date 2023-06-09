[System.Serializable]

public class LevelData
{
    // Define variables to Save
    public bool move, stopTimer;
    public float posX, posY, posZ, controller;

    // Create method of the class to assign values to variables to Save
    public LevelData(ObjectTransitionUp levelObjectData)
    {
        move = levelObjectData.move;
        posX = levelObjectData.transform.position.x;
        posY = levelObjectData.transform.position.y;
        posZ = levelObjectData.transform.position.z;
        controller = levelObjectData.controller;
        stopTimer = levelObjectData.stopTimer;
    }
}
