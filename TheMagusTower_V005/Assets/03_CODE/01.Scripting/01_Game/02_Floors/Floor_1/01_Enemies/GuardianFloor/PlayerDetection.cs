using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    GuardianF1 guardianEnemy;
    [SerializeField] Enemy enemy;

    private void Start()
    {
        guardianEnemy = GetComponentInParent<GuardianF1>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            enemy.playerDetected = true;
            guardianEnemy.attacking = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.playerDetected = true;
            guardianEnemy.attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.playerDetected = false;
            guardianEnemy.attacking = false;
        }
    }
}
