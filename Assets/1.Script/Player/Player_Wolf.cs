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
        yield return new WaitForSeconds(5f);
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

    void EventSwitchAnimation()
    {

    }
}
