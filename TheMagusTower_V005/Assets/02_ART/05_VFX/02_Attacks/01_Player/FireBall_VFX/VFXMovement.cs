using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXMovement : MonoBehaviour
{
    [Tooltip("Set Spell as a Child of the spell initial position")]
    [SerializeField] internal Transform initialPosition, direction;
    [SerializeField] internal float speed, rotation;
    Enemy enemyStats;
    float timer;

    private void OnEnable()
    {
        transform.position = initialPosition.position;
        transform.LookAt(direction);
    }
    private void Start()
    {
        transform.position = initialPosition.position;
        transform.LookAt(direction);

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(speed != 0)
        {
            //rb.AddForce(transform.forward * (speed * Time.deltaTime));
            transform.position += transform.forward * (speed * Time.deltaTime);
            transform.Rotate(0, 0, rotation);
        }
        if(timer > 5)
        {
            timer = 0;
            transform.position = initialPosition.position;
            transform.LookAt(direction);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            gameObject.SetActive(false);
        }
        else if(other.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
        else if(other.tag == "Enemy")
        {
            Debug.Log(other.name);
            enemyStats = other.GetComponent<Enemy>();
            enemyStats.health -= 50;
            Debug.Log(enemyStats.health);
            gameObject.SetActive(false);
        }
    }
}
