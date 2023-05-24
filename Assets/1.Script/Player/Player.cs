using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected enum AnimationIndex
    {
        littlebone,
        nohead,
        wolf
    }
    public enum PlayerDir
    {
        left,
        right
    }
    public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    protected Rigidbody2D rigid;
    public Animator animator;

    public List<RuntimeAnimatorController> animators;

    protected Skill_Head head;
    protected IEnumerator cor;

    protected float originalGravity = 6;

    public float moveSpeed = 15f;

    int maxDashCount = 2;
    float dashPower = 30f;
    float dashCoolTime = 0.8f;
    float dashTime = 0.2f;
    protected bool canDash = true;
    bool isDashing = false;

    float jumpPower = 24f;
    bool isGround = true;
    bool jumped = false;

    protected bool canSkill_1 = true;
    protected bool canSkill_2 = true;

    public bool isSwitched = false;

    protected abstract void Init();
    

    void Start()
    {
        Init();
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

    protected void LookDir()
    {
        switch (playerDir)
        {
            case PlayerDir.right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case PlayerDir.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
        playerDir_past = playerDir;
    }

    protected void JumpAnimation()
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

    protected void Move()
    {
        if (isDashing || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump_Attack"))
            return;

        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * (x * Time.deltaTime * moveSpeed));
        //rigid.velocity = new Vector2( x * moveSpeed, rigid.velocity.y);

        if(x == 0)
            animator.SetBool("Walk", false);
        else
        {
            animator.SetBool("Walk", true);
            playerDir = x > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }
    }

    protected void Jump()
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

    protected IEnumerator Dash()
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

    protected void Attack()
    {
        if (isDashing)
            return;

        animator.SetTrigger("Attack");
    }

    protected abstract IEnumerator Skill_1();
    protected abstract IEnumerator Skill_2();
    protected abstract void SwitchSkill();

    public void ResetCool()
    {
        StopCoroutine(cor);
        canSkill_1 = true;
    }

    protected void SkulSwitch()
    {

    }

    protected void SetGravity(bool On)
    {
        rigid.gravityScale = On ? originalGravity : 0;
    }

    

    protected void EventMoveAttack()
    {
        if ((Input.GetAxisRaw("Horizontal") > 0 && playerDir_past == PlayerDir.right)||
            (Input.GetAxisRaw("Horizontal") < 0 && playerDir_past == PlayerDir.left))
            rigid.velocity = transform.right * moveSpeed + transform.up * rigid.velocity.y;
    }

    protected void EventStopMoveAttack()
    {
        if (!isDashing)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 0.5f);
        if (collision.gameObject.CompareTag("Ground") && hit.collider != null)
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