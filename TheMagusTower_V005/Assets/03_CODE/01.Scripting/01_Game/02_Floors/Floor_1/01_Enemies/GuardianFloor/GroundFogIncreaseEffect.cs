using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFogIncreaseEffect : MonoBehaviour
{
    ParticleSystem fog;
    ParticleSystem.ShapeModule fogShape;
    float fogMinRadius = 0.1f, fogMaxRadius = 3f, fogGrowthRatio = 0.03f;

    private void Start()
    {
        fog = GetComponent<ParticleSystem>();
        fogShape = fog.shape;
        fogShape.radius = fogMinRadius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fogShape.radius < fogMaxRadius)
        {
            fogShape.radius += fogGrowthRatio;
        }
    }
}
