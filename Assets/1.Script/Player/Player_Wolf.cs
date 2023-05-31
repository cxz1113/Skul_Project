using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf : Player
{
    protected override void Init()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.wolf];
        rigid = GetComponent<Rigidbody2D>();

        if (isSwitched)
        {
            SwitchSkill();
        }

        switchIndex = 0;
    }

    protected override IEnumerator Skill_1()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        yield return new WaitForSeconds(3f);
        canSkill_1 = true;
    }

    protected override IEnumerator Skill_2()
    {
        yield break;
    }

    protected void EventSkill()
    {
        
    }

    protected override void SwitchSkill()
    {
        LookDir();
        animator.SetTrigger("Switch");
    }

    //스위치 시작하자마자 event
    void EventSwitchAnimation()
    {
        Teleport(10);
    }

    void Teleport(float distance)
    {
        int layer = 1<<LayerMask.NameToLayer("Ground");
        RaycastHit2D hit1 = Physics2D.Raycast(gameObject.transform.position, transform.right, distance , layer);
        RaycastHit2D hit2 = Physics2D.Raycast(gameObject.transform.position + (capCol.size.y) * transform.up, transform.right, distance, layer);

        if (!hit1 && !hit2)
        {
            transform.Translate(distance * Vector2.right);
        }
        else if (hit1 == hit2)
        {
            transform.Translate((hit1.distance - capCol.size.x / 2) * Vector2.right);
        }
    }
}
