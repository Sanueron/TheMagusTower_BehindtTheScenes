using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            enemy.playerDetected = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            enemy.playerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            enemy.playerDetected = false;
        }
    }
    #endregion
}
