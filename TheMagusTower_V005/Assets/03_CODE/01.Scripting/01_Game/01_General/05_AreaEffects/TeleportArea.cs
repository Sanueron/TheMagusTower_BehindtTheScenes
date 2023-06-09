using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    [Tooltip("Destination of teleportation")]
    [SerializeField] GameObject destination;
    // Player variable
    Rigidbody playerRigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            Debug.Log("Telport");
            playerRigidBody = other.GetComponent<Rigidbody>();
            playerRigidBody.position = destination.transform.position;
        }
    }
}
