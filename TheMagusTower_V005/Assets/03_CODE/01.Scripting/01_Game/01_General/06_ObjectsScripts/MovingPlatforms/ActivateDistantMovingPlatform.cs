using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDistantMovingPlatform : MonoBehaviour
{
    [SerializeField] private MovingPlatforms movingPlatforms;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerCheck")
        {
            movingPlatforms.move = true;
        }
    }
}
