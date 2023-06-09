using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransitionUp : MonoBehaviour
{
    [SerializeField] internal float controller, timeToDestination;
    [SerializeField] internal bool move, stopTimer;
    internal Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            move = false;
            MoveGameObject();
        }
    }

    public void MoveGameObject()
    {
        if (!stopTimer)
        {
            controller += Time.deltaTime;
            if (controller <= timeToDestination)
            {
                transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            }
            else
            {
                stopTimer = true;
            }
        }
    }
}
