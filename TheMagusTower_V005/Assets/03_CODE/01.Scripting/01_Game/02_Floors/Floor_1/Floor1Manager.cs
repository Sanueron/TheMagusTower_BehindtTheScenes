using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor1Manager : MonoBehaviour
{
    [SerializeField] internal GameObject _floorEntrance, _floorLevelDoor, _floorGuardian;
    internal ObjectTransitionUp floorEntrance, floorLevelDoor;
    internal Enemy guardian;

    public bool openLevelEntrance, openDoor;

    private void Start()
    {
        floorEntrance = _floorEntrance.transform.GetComponent<ObjectTransitionUp>();
        floorLevelDoor = _floorLevelDoor.transform.GetComponent<ObjectTransitionUp>();
        guardian = _floorGuardian.transform.GetComponent<Enemy>();
        // activando el level entrance activamos el resto. Lo activamos cuando queremos que suba la entrada
        openLevelEntrance = true;
    }

    private void Update()
    {
        UpEntrance();
        UpDoor();
    }

    private void UpEntrance()
    {
        if (openLevelEntrance)
        {
            floorEntrance.move = true;
        }
    }
    private void UpDoor()
    {
        if (guardian.isDying)
        {
            openDoor = true;
            if (openDoor)
            {
                floorLevelDoor.move = true;
                openDoor = false;
            }
        }
        else
        {
            openDoor = false;
        }
        
    }
}
