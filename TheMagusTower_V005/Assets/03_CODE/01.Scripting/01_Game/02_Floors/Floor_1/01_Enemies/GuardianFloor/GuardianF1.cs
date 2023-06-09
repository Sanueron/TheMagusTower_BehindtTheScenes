using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardianF1 : GuardianManager
{
    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        EnemyState(enemyState);
    }
}
