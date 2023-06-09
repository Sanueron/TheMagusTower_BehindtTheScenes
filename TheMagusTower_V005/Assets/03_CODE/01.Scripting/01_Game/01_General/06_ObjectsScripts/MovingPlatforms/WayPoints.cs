using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    #region WayPoints Methods
    // Method to find the first wayPoint 
    public Transform GetWayPoint(int wayPointIndex)
    {
        return transform.GetChild(wayPointIndex);
    }

    // Method to get the next way point
    public int GetNextWayPoint(int currentWayPoint)
    {
        int nextWayPoint = currentWayPoint + 1;

        if (nextWayPoint == transform.childCount)
        {
            nextWayPoint = 0;
        }

        return nextWayPoint;
    }
    #endregion
}
