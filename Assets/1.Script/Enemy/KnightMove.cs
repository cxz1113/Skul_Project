using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : Enemy
{
    public override void Init()
    {
        ed.entype = EnemyType.knigt;
        ed.rayY = 1.2f;
    }

    void Start()
    {
        Init();
    }
}
