using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : Enemy
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

    IEnumerator EventTackle()
    {
        rigid.AddForce(transform.right * 30 * rigid.mass * transform.localScale.x, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.15f);
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    public override void Damaged(float damage)
    {
        
        if (ed.state != EnemyState.Dead)
        {
            ed.hp -= damage;
            CreateDamage_Text(damage);
            //스컬에 따른 이펙트
            CreateFx_Effect();

            if (ed.state != EnemyState.Attack)
            {
                anim.SetTrigger("Hit");
                ed.state = EnemyState.Hit;
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }
        }
    }

    IEnumerator TackleCoolDown()
    {
        canTackle = false;
        yield return new WaitForSeconds(tackleDelay);
        canTackle = true;
    }
}
