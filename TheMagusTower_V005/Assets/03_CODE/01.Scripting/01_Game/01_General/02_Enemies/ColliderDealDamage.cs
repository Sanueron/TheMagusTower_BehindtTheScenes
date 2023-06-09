using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDealDamage : MonoBehaviour
{
    internal PlayerController player;
    internal Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponentInParent<Enemy>();
    }

    internal void DealDamage(int damage)
    {
        if (!player.isInvincible)
        {
            player.health -= damage;
            player.receivedDamage = true;
        }
        else { player.receivedDamage = false; }
    }

    private void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // Particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        // Get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var colliderData);

        // Iterate
        for(int i = 0; i < numEnter; i++)
        {
            //Debug.Log(colliderData.GetCollider(i, 0).transform.name);
            if(colliderData.GetCollider(i,0).transform.tag == "Player")
            {
                DealDamage(enemy.damage);
            }
        }

        // Set
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

}
