using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerMove : Enemy
{
    public override void Init()
    {
        ed.entype = EnemyType.Tanker;
        ed.rayY = 0;
    }

    void Start()
    {
        Init();
    }
}
