using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInGameMessages : MonoBehaviour
{
    [SerializeField] GameObject message;
    public float timeMessageDisplays;

    // Start is called before the first frame update
    void Start()
    {
        if (message.activeSelf)
        {
            message.SetActive(false);
        }
    }
    IEnumerator SetMessageOff(float time)
    {
        time = 3;
        yield return new WaitForSeconds(time);
        if (message.activeSelf)
        {
            message.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            message.SetActive(true);
            StartCoroutine("SetMessageOff", timeMessageDisplays);
            // Corrutina para desactivar
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            message.SetActive(false);
        }
    }
}
