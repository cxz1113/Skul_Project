using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword : Player
{

    protected override void Init()
    {
        base.Init();
        Damage = 15;
        Damage_Skill1 = 40;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.sword];
        rigid = GetComponent<Rigidbody2D>();

        if (isSwitched)
        {
            SwitchSkill();
            StartCoroutine(CCoolDown_UI(ProjectManager.Instance.ui.switch_Mask, 0));
        }

        switchIndex = 2;
    }

    protected override IEnumerator CSkill_1()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        StartCoroutine(CCoolDown_UI(ProjectManager.Instance.ui.skill1_Mask, 1));
        yield return new WaitForSeconds(1f);
        canSkill_1 = true;
    }


    protected override IEnumerator CSkill_2()
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
        isSwitched = false;
    }

    //스위치 시작하자마자 event
    void EventSwitchAnimation()
    {
        //Teleport_Attack(10);
    }
}
