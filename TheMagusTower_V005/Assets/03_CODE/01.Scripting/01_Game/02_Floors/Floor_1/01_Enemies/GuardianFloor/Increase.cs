using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Increase : ColliderDealDamage
{
    new SphereCollider collider;
    public bool fogHarm;
    int fogDamageMultiplier = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponentInParent<Enemy>();
        collider = GetComponent<SphereCollider>();
        collider.radius += (float)0.03;
    }

    // Update is called once per frame
    void Update()
    {
        if(collider.radius < 5f)
        {
            collider.radius += (float)0.03;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (fogHarm)
            {
                Debug.Log("I can't breath with this fog");
                player.health -= enemy.damage * fogDamageMultiplier;
                fogHarm = false;
                StartCoroutine("FogDamage");
            }
        }
    }

    IEnumerator FogDamage()
    {
        yield return new WaitForSeconds(2f);
        fogHarm = true;
    }
}
