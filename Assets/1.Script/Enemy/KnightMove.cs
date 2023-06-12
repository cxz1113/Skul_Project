using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : Enemy_Test
{
    public override void Init()
    {
        ed.entype = EnemyType.knigt;
        ed.rayY = 1.2f;

        ed.maxhp = 50;
        ed.hp = 50;
        ed.damage = 5;
    }

    void Start()
    {
        Init();
    }
}
