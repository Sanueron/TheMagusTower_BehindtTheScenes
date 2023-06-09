using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    internal PlayerController playerStats;

    // The WarpGates that are at the bottom of the level inside the mist
    internal GameObject[] mistWarps;

    // Start is called before the first frame update
    internal virtual void Start()
    {
        playerStats = FindObjectOfType<PlayerController>();
    }

    internal void DealDamageOverTime(bool condition, int damage)
    {
        if (condition)
        {
            if (!playerStats.isInvincible)
            {
                playerStats.health -= damage;
                playerStats.isInvincible = true;
                playerStats.invincibilityTimer = 0;
            }

        }
    }
    internal void DeactivateGOArray()
    {
        for(int i = 0; i < mistWarps.Length; i++)
        {
            mistWarps[i].SetActive(false);
        }
    }
}
