using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereController : MonoBehaviour
{
    [SerializeField] private GameObject energySemiSphere, controller, panel, message;

    private void Start()
    {
        message.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            panel.SetActive(true);
            message.SetActive(true);
            //Debug.Log("Press C to destroy");
            if (Input.GetKeyDown(KeyCode.C))
            {
                message.SetActive(false);
                panel.SetActive(false);
                energySemiSphere.SetActive(false);
                controller.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            message.SetActive(false);
            panel.SetActive(false);
            
        }
    }
}
