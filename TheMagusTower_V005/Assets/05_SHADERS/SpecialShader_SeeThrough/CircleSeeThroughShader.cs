using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSeeThroughShader : MonoBehaviour
{
    //public static int posID = Shader.PropertyToID("_Position");
    public static int sizeID = Shader.PropertyToID("_Size");


    public Material wallMaterial;
    public new Camera camera;
    public LayerMask mask;
    RaycastHit hit;
    public float rayDistance;

    // Update is called once per frame
    void Update()
    {
        RayCastDetection();
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(camera.transform.position, transform.forward * 10f);
    }
    */
    private void RayCastDetection()
    {
        if(Physics.Raycast(camera.transform.position, camera.transform.forward,out hit, rayDistance, mask, QueryTriggerInteraction.Ignore))
        {
            if(hit.transform.tag == "Wall")
            {
                wallMaterial.SetFloat(sizeID, 2);
                string name = hit.collider.gameObject.name;
                //Debug.DrawRay(camera.transform.position, Vector3.forward * 4.5f, Color.green);
                //Debug.Log(name);
            }
            else
            {
                wallMaterial.SetFloat(sizeID, 0);
                string name = hit.collider.gameObject.name;
                Debug.DrawRay(camera.transform.position, camera.transform.forward * 4.5f, Color.red);
            }
        }
        else
        {
            wallMaterial.SetFloat(sizeID, 0);
        }
    }
}
