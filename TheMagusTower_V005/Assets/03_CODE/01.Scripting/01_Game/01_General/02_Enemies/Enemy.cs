using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyClass
{
    
    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        enemyAnimator = GetComponent<Animator>();
        FindTypeAndClass(type);
        finalType = type.ToString();
        launchNormalAttack = true;
        if(vfxNormalAttack != null)
        {
            vfxNormalAttack.Stop();
        }
        if(vfxSpecialAttack != null)
        {
            vfxSpecialAttack.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealthControl();
        if (isDying)
        {
            enemyAnimator.SetBool("Attacking", false);
            enemyAnimator.SetBool("Move", false);
            enemyAnimator.SetBool("Dead", true);
        }
        if (playerDetected)
        {
            GoToPlayer();
        }
        else
        {
            if(navMesh != null)
            {
                navMesh.SetDestination(initialPosition);
            }
        }
        EnemyBehaviour(type);
        if (playerStats.isDead)
        {
            playerDetected = false;
        }
    }

    private void GoToPlayer()
    {
        if (!isDying)
        {
            // Creamos una variable local que calcula la diferencia entre la posición del objeto y la del objetivo
            Vector3 enemyDir = playerGO.transform.position - transform.position;
            enemyDir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, enemyDir.normalized, speed * Time.deltaTime);
            if (navMesh != null)
            {
                navMesh.SetDestination(playerGO.transform.position);
            }
        }
    }
}
