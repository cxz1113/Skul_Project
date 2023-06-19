using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerMove : Enemy
{
    public override void Init()
    {
        ed.type = EnemyType.Tanker;
        ed.state = EnemyState.Idle;

        ed.maxhp = 150;
        ed.hp = ed.maxhp;
        ed.damage = 10;

        ed.atkRange = 3;
        ed.atkDelay = 3;
    }

    bool canTackle = true;
    float tackleDelay = 10;

    void Start()
    {
        Init();
    }
    protected override void AttackStart()
    {
        StartCoroutine("AttackCoolDown");
        nextMove = 0;

        if (canTackle)
        {
            StartCoroutine("TackleCoolDown");
            anim.SetTrigger("Tackle");
            ed.state = EnemyState.Attack;
        }
        else
        {
            anim.SetTrigger("Attack");
            ed.state = EnemyState.Attack;
        }

    }

    IEnumerator TackleCoolDown()
    {
        canTackle = false;
        yield return new WaitForSeconds(tackleDelay);
        canTackle = true;
    }
}
