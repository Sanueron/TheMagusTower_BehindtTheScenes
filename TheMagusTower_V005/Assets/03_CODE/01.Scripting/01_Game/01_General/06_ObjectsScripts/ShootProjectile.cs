using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject gO_Player, gO_Enemy;
    PlayerController script_Player;
    Enemy enemy;

    private void Awake()
    {
        script_Player = gO_Player.GetComponent<PlayerController>();
        enemy = gO_Enemy.GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        Shooth();
    }

    private void Shooth()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
    private void DealDamage()
    {
        script_Player.health -= enemy.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Ouch");
            DealDamage();
        }
    }
}
