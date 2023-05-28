using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Player : MonoBehaviour
{
    protected enum AnimationIndex
    {
        littleborn,
        nohead,
        wolf
    }
    public enum PlayerDir
    {
        left,
        right
    }
    [HideInInspector] public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    protected Rigidbody2D rigid;
    [HideInInspector] public Animator animator;

    public List<Player> players;
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

    Collision2D collis;
    //[SerializeField] private CapsuleCollider2D playerCollider;

    protected bool canSkill_1 = true;
    protected bool canSkill_2 = true;

    [HideInInspector] public bool isSwitched = false;
    [SerializeField] protected int switchIndex;
    public float curHp;
    public float maxHp;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = value;
            ProjectManager.Instance.ui.hpGage.fillAmount = curHp / maxHp;
            ProjectManager.Instance.ui.curHpTxt.text = string.Format("{0}", curHp);
        }
    }

    public int item;
    public bool isPush;
    protected abstract void Init();
    public void SwitchInit(Player player)
    {
        animators = player.animators;
        isSwitched = true;
        playerDir = player.playerDir;
        ProjectManager.Instance.HeadSwap();
    }

    void Start()
    {
        Init();
    }


    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Skill"))
            return;
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player Switch"))
        {
            float dir = playerDir == PlayerDir.right ? 1 : -1;
            transform.Translate(dir * transform.right * (Time.deltaTime * moveSpeed));
            return;
        }

        Move();
        LookDir();
        JumpAnimation();
        InputDownJump();
        Jump();
        Attack();

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
            StartCoroutine("Dash");

        InputSkill_1();
        InputSkill_2();

        SkulSwitch();
        Test();
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
        //대쉬중이거나 공격(점프공격 제외)중 이동 불가
        if (isDashing || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump_Attack"))
            return;

        float x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2( x * moveSpeed, rigid.velocity.y);

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
        //점프키를 안 누르거나 아래방향+점프 
        if (!Input.GetKeyDown(KeyCode.C) || (Input.GetKeyDown(KeyCode.C) && Input.GetAxisRaw("Vertical") < 0)) 
            return;

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
        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
    }

    protected void InputDownJump()
    {
        if (Input.GetKeyDown(KeyCode.C) && Input.GetAxisRaw("Vertical") < 0)
        {
            //collis = 현재 닿아있는 태그가 "Ground"인 collision
            if (collis != null && collis.gameObject.GetComponent<CompositeCollider2D>())
            {
                StartCoroutine(DownJump());
            }
        }
    }

    //플레이어 콜라이더 변경시 수정필요
    IEnumerator DownJump()
    {
            Collider2D platformCollider = collis.gameObject.GetComponent<CompositeCollider2D>();
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), platformCollider);
            yield return new WaitForSeconds(0.25f);
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), platformCollider, false);
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
        if (!Input.GetKeyDown(KeyCode.X) || isDashing)
            return;

        animator.SetTrigger("Attack");
    }

    protected abstract IEnumerator Skill_1();
    protected abstract IEnumerator Skill_2();
    protected abstract void SwitchSkill();

    protected virtual void InputSkill_1()
    {
        if (Input.GetKeyDown(KeyCode.A) && canSkill_1)
        {
            cor = Skill_1();
            StartCoroutine(cor);
        }
    }

    protected virtual void InputSkill_2()
    {
        if (Input.GetKeyDown(KeyCode.S) && canSkill_2)
            StartCoroutine(Skill_2());
    }

    public void ResetCool()
    {
        StopCoroutine(cor);
        canSkill_1 = true;
    }

    protected void SkulSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player player = Instantiate(players[switchIndex], transform);
            player.transform.SetParent(null);
            player.SwitchInit(this);
            Destroy(gameObject);

            if (head != null)
                Destroy(head.gameObject);
        }
    }

    protected void SetGravity(bool On)
    {
        rigid.gravityScale = On ? originalGravity : 0;
    }

    
    //공격 A,B - 무기를 휘두르는 순간 event
    protected void EventMoveAttack()
    {
        //공격시, 바라보는 방향을 입력하고있으면 전진
        if ((Input.GetAxisRaw("Horizontal") > 0 && playerDir_past == PlayerDir.right)||
            (Input.GetAxisRaw("Horizontal") < 0 && playerDir_past == PlayerDir.left))
            rigid.velocity = transform.right * moveSpeed + transform.up * rigid.velocity.y;
    }

    //공격 A,B - 무기를 휘두른 직후 event
    protected void EventStopMoveAttack()
    {
        if (!isDashing)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collis = collision;
            isGround = true;
            animator.SetBool("IsGround", true);
            jumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collis = null;
            isGround = false;
            animator.SetBool("IsGround", false);
        }
    }
    //
    void Test()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isPush = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            isPush = false;
        }
        if (Input.GetKeyUp(KeyCode.F1))
        {
            PlayerBasket.Instance.HP -= 20;
            Debug.Log(PlayerBasket.Instance.HP);
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            PlayerBasket.Instance.HP += 20;
            Debug.Log(PlayerBasket.Instance.HP);
        }
    }
}