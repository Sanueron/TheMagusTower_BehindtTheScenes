using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : EnemyType
{
    // Enums to detect the enemy class within its Type
    // Monsters Type
    public enum MonsterClass : byte { None, FireGazerPawn, FireGazerEvilEye, SilverGazer };
    [HideInInspector] public MonsterClass _class;
    // Undead Type
    public enum UndeadClass : byte { None, Skeleton, Zombie, ElderLicht };
    [HideInInspector] public UndeadClass _UndeadClass;
    // Esoteric Type
    public enum EsotericCreature : byte { Minotos, Nightmare, Phonix };
    [HideInInspector] public EsotericCreature _EsotericCreature;
    // Golem Type
    public enum Golem : byte { GuardianFloor, ElementalGolem };
    [HideInInspector] public Golem _Golem;

    public string finalClass;

    // Attacks general variables
    internal float charge = 0;


    internal void FindTypeAndClass(Type type)
    {
        switch (type)
        {
            case Type.Undead:
                switch (_UndeadClass)
                {
                    case UndeadClass.Skeleton:
                        finalClass = _UndeadClass.ToString();
                        break;

                    case UndeadClass.Zombie:
                        finalClass = _UndeadClass.ToString();
                        break;

                    case UndeadClass.ElderLicht:
                        finalClass = _UndeadClass.ToString();
                        break;
                }
                break;

            case Type.Monster:
                switch (_class)
                {
                    case MonsterClass.FireGazerPawn:
                        finalClass = _class.ToString();
                        break;

                    case MonsterClass.FireGazerEvilEye:
                        finalClass = _class.ToString();
                        break;

                    case MonsterClass.SilverGazer:
                        finalClass = _class.ToString();
                        break;
                }
                break;

            case Type.EsotericCreature:
                switch (_EsotericCreature)
                {
                    case EsotericCreature.Minotos:
                        finalClass = _EsotericCreature.ToString();
                        break;
                }
                break;

            case Type.Golem:
                switch (_Golem)
                {
                    case Golem.GuardianFloor:
                        finalClass = _Golem.ToString();
                        // Mechanics settled in a different script
                        break;
                }
                break;
        }

    }

    internal void EnemyBehaviour(Type type)
    {
        switch (type)
        {
            case Type.Undead:
                switch (_UndeadClass)
                {
                    case UndeadClass.Skeleton:
                        break;

                    case UndeadClass.Zombie:
                        break;

                    case UndeadClass.ElderLicht:

                        break;
                }
                break;

            case Type.Monster:
                switch (_class)
                {
                    case MonsterClass.FireGazerPawn:
                        break;

                    case MonsterClass.FireGazerEvilEye:
                        if (playerDetected && !isDying && !playerStats.isDead)
                        {
                            CheckIfMoving(8.1f);
                            AttackPatterns(3);
                        }
                        else
                        {
                            isMoving = false;
                            enemyAnimator.SetBool("Move", false);
                            vfxNormalAttack.Stop();
                            vfxSpecialAttack.Stop();
                            timer = 0;
                            charge = 0;
                            launchNormalAttack = true;
                        }
                        break;

                    case MonsterClass.SilverGazer:
                        break;

                }
                break;

            case Type.EsotericCreature:
                switch (_EsotericCreature)
                {
                    case EsotericCreature.Minotos:
                        if (playerDetected && !isDying && !playerStats.isDead)
                        {
                            CheckIfMoving(5f);
                        }
                        else
                        {
                            isMoving = false;
                            enemyAnimator.SetBool("Move", false);
                            isAttacking = false;
                            enemyAnimator.SetBool("Attacking", false);
                        }
                        break;

                    case EsotericCreature.Phonix:
                        if (!isAttacking)
                        {
                            vfxNormalAttack.Stop();
                        }
                        if (playerDetected && !isDying && !playerStats.isDead)
                        {
                            isAttacking = true;
                            VFXNormalAttack();
                        }
                        else
                        {
                            isAttacking = false;
                        }
                        break;
                }
                break;

            case Type.Golem:
                switch (_Golem)
                {
                    case Golem.GuardianFloor:
                        // Mechanics settled in a different script
                        break;
                }
                break;
        }

    }

    internal void CheckIfMoving(float stoppingDistance)
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, playerGO.transform.position);
        if (distanceToPlayer >= stoppingDistance)
        {
            isAttacking = false;
            enemyAnimator.SetBool("Attacking", false);
            isMoving = true;
            enemyAnimator.SetBool("Move", true);
        }
        else if (distanceToPlayer < stoppingDistance)
        {
            isMoving = false;
            enemyAnimator.SetBool("Move", false);
            isAttacking = true;
            enemyAnimator.SetBool("Attacking", true);

        }
    }
    internal void AttackPatterns(int specialAttackDuration)
    {
        if (isAttacking)
        {
            if (launchNormalAttack)
            {
                timer += Time.deltaTime;
                vfxNormalAttack.Play();
                if (timer > 5)
                {
                    // Activar ataque de carga
                    Debug.Log("ataque de carga");
                    launchNormalAttack = false;
                    vfxNormalAttack.Stop();
                    vfxSpecialAttack.Play();
                }
            }
            if (vfxSpecialAttack.isPlaying)
            {
                //specialAttackDuration = 3;
                charge += Time.deltaTime;
                if (charge >= specialAttackDuration)
                {
                    vfxSpecialAttack.Stop();
                    timer = 0;
                    launchNormalAttack = true;
                    charge = 0;
                }
            }
        }
        else if (!isAttacking)
        {
            vfxNormalAttack.Stop();
            vfxSpecialAttack.Stop();
            timer = 0;
            charge = 0;
            launchNormalAttack = true;
        }
    }

    internal void VFXNormalAttack()
    {
        if (isAttacking)
        {
            vfxNormalAttack.Play();
        }
        else
        {
            vfxNormalAttack.Stop();
        }
    }
}
