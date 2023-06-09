using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttack : MonoBehaviour
{
    // Variables for setting bubble path
    public Vector3 destination;
    public GameObject bubbleEndPoint, bubbleStartPoint;

    // Variable to access bubble animator
    Animator bubbleAnim;

    // Bool variables for state control
    public bool expand;

    // Float variables for timers
    public float time;
    [SerializeField] private float speed, lerpTime, finalLerpTime;

    // Bubble Damage
    private int bubbleDamage;

    // Variable to access enemy script
    Enemy enemy;


    // On Awake Bubble Start & End point is set to bubbleEndPoint and expand bool to false
    private void OnEnable()
    {
        bubbleStartPoint = GameObject.Find("StartPosBasicSpell");
        bubbleEndPoint = GameObject.Find("BubbleEndPosition");
        bubbleAnim = GetComponent<Animator>();
        transform.position = bubbleStartPoint.transform.position;
        destination = bubbleEndPoint.transform.position;
        expand = false;
        time = 0;
        bubbleDamage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 3)
        {
            expand = true;
        }
        else
        {
            time += Time.deltaTime;
        }
        if (!expand)
        {
            lerpTime = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, lerpTime/finalLerpTime);
        }
        else
        {
            bubbleAnim.SetBool("Expand", true);
            StartCoroutine("BubbleOff");
        }

    }

    IEnumerator BubbleOff()
    {
        yield return new WaitForSeconds(1f);
        bubbleAnim.SetBool("Expand", false);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            enemy = other.GetComponent<Enemy>();
            enemy.health -= bubbleDamage;
            expand = true;
            Debug.Log(enemy.health);
        }
    }
}
