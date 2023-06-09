using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseRangeAttack : MonoBehaviour
{
    public GameObject closeRangeAttack;

    private void Start()
    {
        closeRangeAttack.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            closeRangeAttack.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            closeRangeAttack.SetActive(false);
        }
    }

}
