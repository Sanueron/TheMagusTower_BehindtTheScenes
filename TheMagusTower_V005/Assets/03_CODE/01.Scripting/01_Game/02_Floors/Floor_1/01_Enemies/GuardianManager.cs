using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardianManager : MonoBehaviour
{
    //Main stats of enemies
    internal Enemy guardian;
    [SerializeField] internal int level;
    [SerializeField] internal int health;
    [SerializeField] internal int minHealth, maxHealth, damage, speed;
    //Añadir variable del script del jugador para poder acceder a sus variables.

    // Variable tipo enum para definir los estados y variable de la variable States para asignacion
    internal enum States { iddle, attacking, rampage, dying };
    internal States enemyState;

    // booleanos de control y control del tiempo entre ataques
    [SerializeField] internal bool attacking, rampage, dying;
    internal float timer;

    internal Animator enemiesAnimator;
    [SerializeField] internal NavMeshAgent enemy;

    // Variables GameObject para controlar los VFX y gameobjeccts de los ataques
    internal GameObject iceAttack;
    internal ParticleSystem particleFireAttack;

    // Variable GameObject para encontrar al jugador y para indicar el aro de ataque
    [SerializeField] internal GameObject player, attackingCircle, playerCenter;
    internal PlayerController playerScript;

    // Variables contador de tiempos
    [SerializeField] internal float time;

    // Variables de posicion y rotacion
    internal Vector3 actualPosition;

    internal virtual void Start()
    {
        guardian = GetComponent<Enemy>();
        enemiesAnimator = GetComponent<Animator>();
        particleFireAttack = GetComponentInChildren<ParticleSystem>();
        enemy = GetComponent<NavMeshAgent>();
        iceAttack = GameObject.Find("IceAttack");
        playerScript = player.transform.GetComponentInParent<PlayerController>();
        //fireAttack.SetActive(false);
        particleFireAttack.Stop();
        iceAttack.SetActive(false);
    }

    // Main Enemies Functions
    #region Main Functions

    internal void EnemyState(States enemyState)
    {
        switch (enemyState)
        {
            case States.iddle:
                enemiesAnimator.SetInteger("SelectState", 0);
                break;

            case States.attacking:
                EnemyAttacks();
                break;

            case States.rampage:
                break;

            case States.dying:
                enemiesAnimator.SetBool("Dead", true);
                guardian.isDying = true;

                break;

            default:
                break;
        }
    }
    // Funcion para determinar el estado del enemigo
    internal void ChangeState()
    {
        if(!playerScript.isDead)
        {
            // Establecemos las condiciones para activar cada estado
            if (attacking && !guardian.isDying)
            {
                enemyState = States.attacking;
            }
            else if (rampage && !guardian.isDying)
            {
                enemyState = States.rampage;
                particleFireAttack.Stop();
                StopAllCoroutines();
            }
            else if (guardian.isDying)
            {
                enemyState = States.dying;
                //fireAttack.SetActive(false);
                particleFireAttack.Stop();
                StopAllCoroutines();
                iceAttack.SetActive(false);
            }
            else
            {
                enemyState = States.iddle;
                //fireAttack.SetActive(false);
                particleFireAttack.Stop();
                StopAllCoroutines();
                iceAttack.SetActive(false);
            }
        }
        else
        {
            enemyState = States.iddle;
            //fireAttack.SetActive(false);
            particleFireAttack.Stop();
            StopAllCoroutines();
            iceAttack.SetActive(false);
        }

    }

    //Mediante un random y una variable tipo int hacemos que el enemigo ataque de forma aleatoria
    internal void EnemyAttacks()
    {

        // Creamos una variable local que calcula la diferencia entre la posición del objeto y la del objetivo
        Vector3 enemyDir = player.transform.position - transform.position;
        enemyDir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, enemyDir.normalized, speed * Time.deltaTime);

        enemy.SetDestination(player.transform.position);
        timer += Time.deltaTime;
        int var = Random.Range(1, 3);
        if(timer > 3)
        {
            switch (var)
            {
                case 1:
                    //Fire Attack
                    iceAttack.SetActive(false);
                    enemiesAnimator.SetInteger("SelectState", 1);
                    particleFireAttack.Play();
                    timer = 0;
                    break;
                case 2:
                    //Ice Attack
                    particleFireAttack.Stop();
                    enemiesAnimator.SetInteger("SelectState", 2);
                    StartCoroutine("IceAttack");
                    timer = 0;
                    break;
                case 3:
                    particleFireAttack.Stop();
                    StopAllCoroutines();
                    // transform.LookAt(player.transform.position)
                    enemiesAnimator.SetInteger("SelectState", 3);
                    timer = 0;
                    break;
                case 4:
                    particleFireAttack.Stop();
                    StopAllCoroutines();
                    // transform.LookAt(player.transform.position)
                    enemiesAnimator.SetInteger("SelectState", 4);
                    timer = 0;
                    break;
                default:
                    particleFireAttack.Stop();
                    StopAllCoroutines();
                    enemiesAnimator.SetInteger("SelectState", 0);
                    break;
            }
            Debug.Log(var);
        }
        else { return; }

    }
    #endregion

    IEnumerator IceAttack()
    {
        yield return new WaitForSeconds(1f);
        iceAttack.transform.position = attackingCircle.transform.position;
        iceAttack.transform.LookAt(playerCenter.transform.position);
        iceAttack.SetActive(true);
    }

}
