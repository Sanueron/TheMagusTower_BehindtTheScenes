using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyPlatforms : MonoBehaviour
{
    GameObject[] trickyPlatformsInitialTransform;
    Rigidbody[] platformRigidbodies;
    float[] posX, posY, posZ;
    // Start is called before the first frame update
    void Start()
    {
        trickyPlatformsInitialTransform = new GameObject[transform.childCount];
        platformRigidbodies = new Rigidbody[transform.childCount];
        posX = new float[transform.childCount];
        posY = new float[transform.childCount];
        posZ = new float[transform.childCount];

        for(int i =0; i< transform.childCount; i++)
        {
            platformRigidbodies[i] = transform.GetChild(i).GetComponent<Rigidbody>();
            if (!platformRigidbodies[i].isKinematic)
            {
                posX[i] = platformRigidbodies[i].position.x;
                posY[i] = platformRigidbodies[i].position.y;
                posZ[i] = platformRigidbodies[i].position.z;
            }
        }
    }
    public void ResetTrickyPlatforms()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!platformRigidbodies[i].isKinematic)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                transform.GetChild(i).GetComponent<Rigidbody>().position = new Vector3(posX[i], posY[i], posZ[i]);
                transform.GetChild(i).GetComponent<Rigidbody>().rotation = new Quaternion(0,0,0, 1);
            }
        }
    }
}
