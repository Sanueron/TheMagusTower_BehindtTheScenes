using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMovingPlatform : MonoBehaviour
{
    MovingPlatforms movingPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        movingPlatforms = GetComponent<MovingPlatforms>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "PlayerCheck")
        {
            movingPlatforms.move = true;
        }
    }
}
