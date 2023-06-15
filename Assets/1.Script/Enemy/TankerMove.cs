using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerMove : Enemy
{
    public override void Init()
    {
        ed.type = EnemyType.Tanker;
        ed.state = EnemyState.Idle;
        ed.rayY = 0;

        ed.maxhp = 150;
        ed.hp = ed.maxhp;
        ed.damage = 10;

        ed.atkRange = 3;
        ed.atkDelay = 3;
    }

    void Start()
    {
        Init();
    }
}
