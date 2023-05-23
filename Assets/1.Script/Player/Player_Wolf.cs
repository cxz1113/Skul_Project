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
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Skill"))
            return;

        Move();
        LookDir();
        JumpAnimation();
        if (Input.GetKeyDown(KeyCode.C))
            Jump();

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine("Dash");
        }

        if (Input.GetKeyDown(KeyCode.A) && canSkill_1)
        {
            cor = Skill_1();
            StartCoroutine(cor);
        }

        if (Input.GetKeyDown(KeyCode.S) && canSkill_2 && head != null)
        {
            StartCoroutine(Skill_2());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(GetComponent<Player_Wolf>());
            gameObject.AddComponent<Player_LittleBorn>().animators = animators;
            GetComponent<Player_LittleBorn>().isSwitched = true;
            GetComponent<Player_LittleBorn>().playerDir = playerDir;
        }
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
        
    }
}
