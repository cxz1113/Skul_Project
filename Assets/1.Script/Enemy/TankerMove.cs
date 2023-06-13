using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerMove : Enemy
{
    public override void Init()
    {
        ed.type = EnemyType.Tanker;
        ed.rayY = 0;

        ed.maxhp = 150;
        ed.hp = ed.maxhp;
        ed.damage = 10;
    }

    void Start()
    {
        Init();
    }
}
