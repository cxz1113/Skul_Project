using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : Enemy
{
    public override void Init()
    {
        ed.type = EnemyType.knigt;
        ed.state = EnemyState.Idle;
        ed.rayY = 1.2f;

        ed.maxhp = 50;
        ed.hp = 50;
        ed.damage = 5;

        ed.atkRange = 3;
        ed.atkDelay = 3;
    }

    void Start()
    {
        Init();
    }
}
