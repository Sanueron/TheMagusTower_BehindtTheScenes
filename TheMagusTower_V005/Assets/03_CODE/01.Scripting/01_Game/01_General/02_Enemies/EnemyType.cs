using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyType : EnemiesManager
{
    public enum Type { Undead, Monster, EsotericCreature, Golem };
    [HideInInspector]
    public Type type;

    public string finalType;

    [Tooltip("Assign FX of the attack, not gameObject")]
    [SerializeField] internal ParticleSystem vfxNormalAttack, vfxSpecialAttack;

    // Bool Attacks Types variables
    internal bool launchNormalAttack, launchSpecialAttack;

    // Type variables

    internal void UndeadDefault(int defaultSpeed)
    {
        speed = defaultSpeed * 1;
    }
}
