using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] Enemy_Projectile arrowPrf;

    public override void Init()
    {
        ed.type = EnemyType.Archer;
        ed.state = EnemyState.Idle;

        ed.maxhp = 50;
        ed.hp = ed.maxhp;
        ed.damage = 5;

        ed.atkRange = 8;
        ed.atkDelay = 3;
    }

    void EventFire()
    {
        Enemy_Projectile arrow = Instantiate(arrowPrf, transform);
        arrow.transform.position += Vector3.up * capsuleColl.size.y * 0.5f;
        arrow.dir = transform.localScale.x > 0 ? 1 : -1;
        arrow.transform.SetParent(null);
    }
}
