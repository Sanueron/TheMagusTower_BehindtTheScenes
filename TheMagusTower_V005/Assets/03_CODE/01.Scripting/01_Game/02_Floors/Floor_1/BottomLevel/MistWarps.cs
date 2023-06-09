using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistWarps : LevelManager
{
    int num;
    // Start is called before the first frame update
    internal override void Start()
    {
        mistWarps = GameObject.FindGameObjectsWithTag("WarpGate");
        DeactivateGOArray();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            num = Random.Range(0, 7);
            mistWarps[num].SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DeactivateGOArray();
        }
    }
}
