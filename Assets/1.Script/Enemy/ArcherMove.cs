using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMove : Enemy
{
    public override void Init()
    {
        ed.type = EnemyType.Archer;
        ed.state = EnemyState.Idle;

        ed.maxhp = 50;
        ed.hp = ed.maxhp;
        ed.damage = 5;

        ed.atkRange = 3;
        ed.atkDelay = 3;
    }

    void Start()
    {
        Init();
    }
}
