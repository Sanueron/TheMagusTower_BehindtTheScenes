using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField]
    private WayPoints _wayPoints;

    [SerializeField] 
    private float speed;

    internal int targetWayPointIndex;

    private float timeToWayPoint;
    private float elapsedTime;

    [SerializeField] internal bool move;


    private Transform previousWayPoint, targetWayPoint;

    private void Start()
    {
        TargetNextWayPoint();
    }
    private void FixedUpdate()
    {
        if (move)
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToWayPoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(previousWayPoint.position, targetWayPoint.position, elapsedPercentage);
            transform.rotation = Quaternion.Lerp(previousWayPoint.rotation, targetWayPoint.rotation, elapsedPercentage);

            // When we reach 100% of the journey we call again the method to know the next waypoint
            if (elapsedPercentage >= 1)
            {
                TargetNextWayPoint();
            }
        }
    }
    private void TargetNextWayPoint()
    {
        previousWayPoint = _wayPoints.GetWayPoint(targetWayPointIndex);
        targetWayPointIndex = _wayPoints.GetNextWayPoint(targetWayPointIndex);
        targetWayPoint = _wayPoints.GetWayPoint(targetWayPointIndex);

        elapsedTime = 0;

        float distanceToWayPoint = Vector3.Distance(previousWayPoint.position, targetWayPoint.position);

        timeToWayPoint = distanceToWayPoint / speed;

    }
    internal void Reset()
    {
        int zero = 0;
        //Reset moving platform to initial state
        move = false;
        elapsedTime = zero;
        targetWayPointIndex = zero;
        previousWayPoint = _wayPoints.transform.GetChild(0);
        targetWayPoint = _wayPoints.transform.GetChild(1);
        transform.position = _wayPoints.transform.GetChild(0).position;
        transform.rotation = _wayPoints.transform.GetChild(0).rotation;
        TargetNextWayPoint();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "PlayerCheck")
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "PlayerCheck")
        {
            collision.transform.SetParent(null);
        }
    }
}
