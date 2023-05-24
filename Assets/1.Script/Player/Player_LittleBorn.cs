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
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littlebone];
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
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player Switch"))
        {
            float dir = playerDir == PlayerDir.right ? 1 : -1;
            transform.Translate(dir * transform.right * (Time.deltaTime * moveSpeed));
            return;
        }

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
            Destroy(GetComponent<Player_LittleBorn>());
            gameObject.AddComponent<Player_Wolf>().animators = animators;
            GetComponent<Player_Wolf>().isSwitched = true;
            GetComponent<Player_Wolf>().playerDir = playerDir;

            if (head!=null)
                Destroy(head.gameObject);
        }
    }

    protected override IEnumerator Skill_1()
    {
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        yield return new WaitForSeconds(5f);
        canSkill_1 = true;
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littlebone];
    }

    protected override IEnumerator Skill_2()
    {
        canSkill_2 = false;
        transform.position = head.transform.position;
        Destroy(head.gameObject);
        ResetCool();
        head = null;
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.littlebone];
        yield return new WaitForSeconds(5f);
        canSkill_2 = true;
    }

    void EventSkill()
    {
        head = Instantiate(prefab_Head, firePos);
        head.coolTime = 5;
        head.dir = playerDir == PlayerDir.right ? 1 : -1;
        head.player = this;
        head.transform.SetParent(head_Parent);
    }

    void EventChangeAnimation()
    {
        animator.runtimeAnimatorController = animators[(int)AnimationIndex.nohead];
    }

    protected override void SwitchSkill()
    {
        LookDir();
        animator.SetTrigger("Switch");
    }

   
}
