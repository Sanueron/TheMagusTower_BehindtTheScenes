using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryArea : MonoBehaviour
{
    PlayerController playerStats;

    // Enum variable to define in inspector which type of recovery is
    internal enum Type { RecoverMP, RecoverHP };
    [SerializeField] internal Type type;

    // Recovery Amount
    [SerializeField] private int recoveryAmount, coolDown;
    private float time;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            playerStats = other.GetComponent<PlayerController>();
            switch (type)
            {
                case Type.RecoverHP:
                    if(playerStats.health < playerStats.maxHealth)
                    {
                        // Activate healing hp aura VFX
                        if (!playerStats.healingHP.isPlaying)
                        {
                            playerStats.healingHP.Play();
                        }
                        time += Time.deltaTime;
                        if (time > coolDown)
                        {
                            playerStats.health += recoveryAmount;
                            time = 0;
                        }
                    }
                    else
                    {
                        playerStats.healingHP.Stop();
                    }
                    break;

                case Type.RecoverMP:
                    if(playerStats.magicPoints < playerStats.maxMP)
                    {
                        // Activate healing mp aura VFX
                        if (!playerStats.healingMP.isPlaying)
                        {
                            playerStats.healingMP.Play();
                        }
                        time += Time.deltaTime;
                        if (time > coolDown)
                        {
                            playerStats.magicPoints += recoveryAmount;
                            time = 0;
                        }
                    }
                    else
                    {
                        playerStats.healingMP.Stop();
                    }
                    break;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            playerStats.healingHP.Stop();
            playerStats.healingMP.Stop();
        }
    }
}
