using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword : Player
{

    protected override void Init()
    {
        stpd.skul = PlayerSkul.Sword;
        Damage = 15;
        Damage_Skill1 = 30;

        base.Init();

        StartCoroutine(Switched());

        switchIndex = 2;
    }

    #region 스킬1
    protected override IEnumerator CSkill_1()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        StartCoroutine(CCoolDown_UI(ProjectManager.Instance.ui.skill1_Mask, 1));
        yield return new WaitForSeconds(1f);
        canSkill_1 = true;
    }
    #endregion

    #region 스킬2
    protected override IEnumerator CSkill_2()
    {
        yield break;
    }
    #endregion

    #region 교대
    protected override void SwitchSkill()
    {
        LookDir();
        animator.SetTrigger("Switch");
        isSwitched = false;
    }

    #endregion
}
