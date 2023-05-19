using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum PlayerDir
    {
        left,
        right
    }
    PlayerDir playerDir = PlayerDir.right;
    PlayerDir playerDir_past;

    Rigidbody2D rigid;
    Animator animator;
    [SerializeField] Skill_Head prefab_Head;
    [SerializeField] Transform firePos;
    [SerializeField] Transform head_Parent;

    [SerializeField] List<RuntimeAnimatorController> animators;

    Skill_Head head;
    IEnumerator cor;

    float originalGravity = 3;

    float moveSpeed = 7f;
    bool canMove = true;

    int maxDashCount = 2;
    float dashPower = 15f;
    float dashCoolTime = 0.8f;
    float dashTime = 0.2f;
    bool canDash = true;
    bool isDashing = false;

    float jumpPower = 12f;
    bool isGround = true;
    bool jumped = false;

    bool canSkill_1 = true;
    bool canSkill_2 = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            animator.runtimeAnimatorController = animators[1];
        }
    }

    void LookDir()
    {
        switch (playerDir)
        {
            case PlayerDir.right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (playerDir_past != playerDir)
                {
                    //transform.Translate(Vector2.right * 0.75f);
                    //transform.Translate(Vector2.right * tempds * Time.deltaTime); 
                }
                break;

            case PlayerDir.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (playerDir_past != playerDir)
                {
                    //transform.Translate(Vector2.right * 0.75f);
                    //transform.Translate(Vector2.right * tempds * Time.deltaTime);

                    //transform.position += transform.TransformVector(Vector3.right) * 1.75f * 2;
                }
                break;
        }
        playerDir_past = playerDir;
    }

    void JumpAnimation()
    {
        if (rigid.velocity.y > 0.05f)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }
        else if (rigid.velocity.y < -0.05f)
        {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }

    void Move()
    {
        if (!canMove)
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump_Attack"))
        {

        }
        else if (isDashing || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            return;

        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * (x * Time.deltaTime * moveSpeed));

        animator.SetBool("Walk", true);

        if(x == 0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            playerDir = x > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }
    }

    void Jump()
    {
        if (!isGround)
        {
            if (jumped)
                return;
            else
            {
                jumped = true;
            }
        }

        rigid.velocity = Vector2.zero;
        SetGravity(true);
        animator.SetBool("Dash", false);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        float inputDir = Input.GetAxisRaw("Horizontal");

        canDash = false;
        isDashing = true;
       
        animator.SetBool("Dash", true);
        SetGravity(false);
        playerDir = inputDir < 0 ? PlayerDir.left : inputDir > 0 ? PlayerDir.right : playerDir;
        float dir = playerDir == PlayerDir.right ? 1 : -1;
        LookDir();
        rigid.velocity = new Vector2(dir * dashPower, 0);

        yield return new WaitForSeconds(dashTime);

        SetGravity(true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"))
            rigid.velocity = Vector2.zero;

        isDashing = false;
        animator.SetBool("Dash", false);

        yield return new WaitForSeconds(dashCoolTime);
        canDash = true;
    }

    void Attack()
    {
        if (isDashing)
            return;

        animator.SetTrigger("Attack");
    }

    IEnumerator Skill_1()
    {
        canSkill_1 = false;
        animator.SetTrigger("Skill_1");
        yield return new WaitForSeconds(5f);
        canSkill_1 = true;
    }

    IEnumerator Skill_2()
    {
        canSkill_2 = false;
        transform.position = head.transform.position;
        Destroy(head.gameObject);
        ResetCool();
        head = null;
        yield return new WaitForSeconds(5f);
        canSkill_2 = true;
    }

    public void ResetCool()
    {
        StopCoroutine(cor);
        canSkill_1 = true;
    }

    public void SetGravity(bool On)
    {
        rigid.gravityScale = On ? originalGravity : 0;
    }

    void EventSkill()
    {
        head = Instantiate(prefab_Head, firePos);
        head.coolTime = 5;
        head.dir = playerDir == PlayerDir.right ? 1 : -1;
        head.player = this;
        head.transform.SetParent(head_Parent);
    }

    void EventMoveAttack()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && playerDir_past == PlayerDir.right)
        {
            rigid.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && playerDir_past == PlayerDir.left)
        {
            rigid.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
    }

    void EventStopMoveAttack()
    {
        if (!isDashing)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            animator.SetBool("IsGround", true);
            jumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            animator.SetBool("IsGround", false);
        }
    }
}