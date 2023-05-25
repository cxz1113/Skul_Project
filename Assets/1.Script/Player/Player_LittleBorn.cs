using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LittleBorn : Player
{
    [SerializeField] Skill_Head prefab_Head;
    [SerializeField] Transform firePos;
    [SerializeField] Transform head_Parent;

    protected override void Init()
    {
        prefab_Head = Resources.Load<Skill_Head>("Prefab/Head");
        firePos = transform.GetChild(0);
        head_Parent = GameObject.Find("Head_Parent").transform;

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littleborn];
        rigid = GetComponent<Rigidbody2D>();

        if (isSwitched)
        {
            SwitchSkill();
        }

        switchIndex = 1;
    }

    protected override IEnumerator Skill_1()
    {
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        yield return new WaitForSeconds(5f);
        canSkill_1 = true;
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littleborn];
    }

    protected override IEnumerator Skill_2()
    {
        canSkill_2 = false;
        transform.position = head.transform.position;
        Destroy(head.gameObject);
        ResetCool();
        head = null;
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littleborn];
        yield return new WaitForSeconds(5f);
        canSkill_2 = true;
    }

    protected override void InputSkill_2()
    {
        if (Input.GetKeyDown(KeyCode.S) && canSkill_2 && head != null)
        {
            StartCoroutine(Skill_2());
        }
    }

    void EventSkill()
    {
        head = Instantiate(prefab_Head, firePos);
        head.coolTime = 5;
        head.dir = playerDir == PlayerDir.right ? 1 : -1;
        head.player = this;
        head.transform.SetParent(head_Parent);
    }

    void EventSwitchAnimation()
    {
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.nohead];
    }

    protected override void SwitchSkill()
    {
        LookDir();
        animator.SetTrigger("Switch");
    }

   
}
