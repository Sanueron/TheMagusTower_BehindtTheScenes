using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : LevelManager
{
    internal bool playerInside;
    [SerializeField] int areaDamage;
    // Start is called before the first frame update
    internal override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        DealDamageOverTime(playerInside, areaDamage);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            playerStats.receivedDamage = false;
            playerInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = false;
        }
    }
}
