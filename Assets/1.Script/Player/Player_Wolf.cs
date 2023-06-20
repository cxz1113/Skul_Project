using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf : Player
{
    protected override void Init()
    {
        base.Init();
        Damage = 15;
        Damage_Skill1 = 40;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.wolf];
        rigid = GetComponent<Rigidbody2D>();

        if (isSwitched)
        {
            SwitchSkill();
        }

        switchIndex = 0;
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
    protected void EventDamage_Skill1()
    {
        foreach (var enemy in atBox.enemies)
        {
            SetDamage(enemy, 40);
        }
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
    }

    //스위치 시작하자마자 event
    void EventSwitchAnimation()
    {
        Teleport_Attack(10);
    }

    void Teleport_Attack(float distance)
    {
        float tempDis;
        int layer = 1<<LayerMask.NameToLayer("Ground");
        RaycastHit2D hit1 = Physics2D.Raycast(gameObject.transform.position, transform.right, distance , layer);
        RaycastHit2D hit2 = Physics2D.Raycast(gameObject.transform.position + (playerCol.size.y) * transform.up, transform.right, distance, layer);

        if (!hit1 && !hit2)
        {
            //transform.Translate(distance * Vector2.right);
            tempDis = distance;
        }
        else if (hit1 == hit2)
        {
            //transform.Translate((hit1.distance - capCol.size.x / 2) * Vector2.right);
            tempDis = hit1.distance - playerCol.size.x / 2;
        }
        else
        {
            tempDis = hit1 ? hit1.distance : hit2.distance;
            if (hit1 && hit2) 
                tempDis = hit1.distance < hit2.distance ? hit1.distance : hit2.distance;
        }

        int layer2 = 1 << LayerMask.NameToLayer("Enemy");
        RaycastHit2D[] hit3 = Physics2D.RaycastAll(gameObject.transform.position + (playerCol.size.y / 2) * transform.up, transform.right, tempDis, layer2);
        
        foreach (var item in hit3)
        {
            if (item.collider.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = item.collider.gameObject.GetComponent<Enemy>();
                SetDamage(enemy, 40);
            }
        }

        tempDis -= playerCol.size.x / 2;
        transform.Translate(tempDis * Vector2.right);
    }
}
