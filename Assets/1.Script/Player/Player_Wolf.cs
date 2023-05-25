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
        yield break;
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
