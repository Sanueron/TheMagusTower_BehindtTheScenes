using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDealDamage : MonoBehaviour
{
    public PlayerController playerStats;
    public Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            playerStats = other.GetComponent<PlayerController>();
            playerStats.health -= enemy.damage;
        }
    }
}
