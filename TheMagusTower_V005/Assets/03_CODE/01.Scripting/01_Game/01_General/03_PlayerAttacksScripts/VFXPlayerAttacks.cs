using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayerAttacks : MonoBehaviour
{
    PlayerController player;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        //enemy = FindObjectOfType<Enemy>();
    }

    private void DealDamage(int damage)
    {
        enemy.health -= damage;
        Debug.Log(enemy.health);
    }

    private void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // Particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        // Get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var colliderData);

        // Iterate
        for (int i = 0; i < numEnter; i++)
        {
            //Debug.Log(colliderData.GetCollider(i, 0).transform.name);
            if (colliderData.GetCollider(i, 0).transform.tag == "Enemy")
            {
                enemy = colliderData.GetCollider(i, 0).GetComponent<Enemy>();
                Debug.Log(colliderData.GetCollider(i, 0).transform.name);
                DealDamage(player.damage);
            }
        }
    }
}
