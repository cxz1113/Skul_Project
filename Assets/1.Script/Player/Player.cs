using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public struct StPlayerData
{
    public PlayerType type;
    public PlayerSkul skul;
}

public enum PlayerSkul
{
    LittleBorn,
    Wolf,
    Sword
}

public enum PlayerType
{
    Balance,
    Speed,
    Power
}


public abstract class Player : MonoBehaviour
{
    public StPlayerData stpd;

    protected enum AnimationIndex
    {
        littleborn,
        nohead,
        wolf,
        sword,
        calleon
    }
    public enum PlayerDir
    {
        left,
        right
    }
    [HideInInspector] public PlayerDir playerDir = PlayerDir.right;
    protected PlayerDir playerDir_past;

    [HideInInspector] public Animator animator;
    protected Rigidbody2D rigid;

    //inspector에서 직접 넣어줄 것들
    public List<Player> players;
    public List<RuntimeAnimatorController> animators;
    public Attack_Box_Player atBox;
    [SerializeField] protected SpriteRenderer spriteRd;
    [SerializeField] protected CapsuleCollider2D playerCol;
    [SerializeField] Enemy enemy;

    protected bool canInput = true;
    protected float originalGravity = 6;

    //Move
    public float moveSpeed = 15f;

    //Dash
    int maxDashCount = 2;
    float dashPower = 30f;
    float dashCoolTime = 0.8f;
    float dashTime = 0.2f;
    protected bool canDash = true;
    bool isDashing = false;

    //Jump
    float jumpPower = 24f;
    bool isGround = true;
    bool jumped = false;

    //DownJump
    Collision2D collis;

    //Atack
    public float Damage { get; set; }
    public float Damage_Skill1 { get; set; }
    public float Damage_Skill2 { get; set; }

    //Damaged
    protected bool isDead = false;
    protected bool isUnbeat = false;
    protected float unbeatTime = 1f;
    

    //Skill

    protected bool canSkill_1 = true;
    protected bool canSkill_2 = true;


    //Switch
    [HideInInspector] public bool isSwitched = false;
    [HideInInspector] public bool canSwitch = false;
    [SerializeField] public int switchIndex;
    
    public bool isPush;

    protected virtual void Init()
    {
        ProjectManager.Instance.ui.skill1_Mask.fillAmount = 0;
        ProjectManager.Instance.ui.skill2_Mask.fillAmount = 0;
        ProjectManager.Instance.ui.switch_Mask.fillAmount = 0;
        atBox.player = this;
    }

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
        if (PlayerBasket.Instance.isInven || isDead)
            return;
        
        JumpAnimation();

        if (!canInput)
            return;

        Move();
        Jump();
        Attack();

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
            StartCoroutine("CDash");

        InputSkill_1();
        InputSkill_2();

        SkulSwitch();
        InvenActive();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Instantiate(enemy, pos, Quaternion.identity);
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
        //대쉬중이거나 공격(점프공격 제외)중 이동 불가
        if (isDashing || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Jump_Attack"))
            return;

        float dir = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2( dir * moveSpeed, rigid.velocity.y);

        if(dir == 0)
            animator.SetBool("Walk", false);
        else
        {
            animator.SetBool("Walk", true);
            playerDir = dir > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }

        LookDir();
    }

    protected void Jump()
    {
        //점프키를 안 누르거나 아래방향+점프 
        if (!Input.GetKeyDown(KeyCode.C))  
            return;
        else if((Input.GetKeyDown(KeyCode.C) && Input.GetAxisRaw("Vertical") < 0))
        {
            DownJump();
            return;
        }

        if (!isGround)
        {
            if (jumped)
                return;

            jumped = true;
        }

        rigid.velocity = Vector2.zero;
        SetGravity(true);
        animator.SetBool("Dash", false);
        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
    }

    protected void DownJump()
    {  
        if (Input.GetKeyDown(KeyCode.C) && Input.GetAxisRaw("Vertical") < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 0.1f, LayerMask.GetMask("Ground"));
            if (!rayHit)
                return;

            if(rayHit.collider.GetComponent<PlatformEffector2D>())
            {
                StartCoroutine(CDownJump(rayHit.collider.GetComponent<CompositeCollider2D>()));
            }
        }
    }

    //플레이어 콜라이더 변경시 수정필요
    IEnumerator CDownJump(Collider2D col)
    {
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), col);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), col, false);
    }

    protected IEnumerator CDash()
    {
        float inputDir = Input.GetAxisRaw("Horizontal");

        canDash = false;
        isDashing = true;
        isUnbeat = true;
       
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
        isUnbeat = false;
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

    protected abstract IEnumerator CSkill_1();
    protected abstract IEnumerator CSkill_2();
    protected abstract void SwitchSkill();

    protected virtual void InputSkill_1()
    {
        if (Input.GetKeyDown(KeyCode.A) && canSkill_1)
            StartCoroutine(CSkill_1());
    }

    protected virtual void InputSkill_2()
    {
        if (Input.GetKeyDown(KeyCode.S) && canSkill_2)
            StartCoroutine(CSkill_2());
    }

    protected IEnumerator CCoolDown_UI(Image mask,float coolTime)
    {
        canSwitch = true;
        float coolDowned = 0;
        while (coolDowned < coolTime)
        {
            yield return new WaitForFixedUpdate();
            coolDowned += Time.deltaTime;
            mask.fillAmount = (coolTime - coolDowned) / coolTime;
        }
        canSwitch = false;
    }

    protected virtual void SkulSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !canSwitch)
        {
            Player player = Instantiate(Resources.Load<Player>($"Player/{ProjectManager.Instance.heads[1].name}"), transform);
            player.transform.SetParent(null);
            player.SwitchInit(this);
            Destroy(gameObject);
            StartCoroutine(CCoolDown_UI(ProjectManager.Instance.ui.switch_Mask, 3));
        }
    }

    public void ItemSwitch(Player plaerN)
    {
        Player player = Instantiate(Resources.Load<Player>($"Player/{plaerN.name}"), transform);
        player.transform.SetParent(null);
        animators = player.animators;
        isSwitched = true;
        playerDir = player.playerDir;
        ProjectManager.Instance.ItemHeadChange();
        Destroy(gameObject);
    }

    protected void SetGravity(bool On)
    {
        rigid.gravityScale = On ? originalGravity : 0;
    }

    //범용 - 현재 실행중 애니메이션이 끝날때까지 입력 불가
    protected IEnumerator EventCStopInput()
    {
        canInput = false;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) 
        {
            yield return new WaitForEndOfFrame();
        }
        canInput = true;
    }

    //공격 A,B - 애니메이션 첫 프레임 event
    protected void EventStopMove()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
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

    //공격 데미지 판정 event
    protected void EventDamage()
    {
        foreach (var enemy in atBox.enemies)
        {
            SetDamage(enemy, Damage);
        }
    }
    public void Damaged(float damage)
    {
        if (isDead || isUnbeat)
            return;

        PlayerBasket.Instance.HP -= damage;
        if (PlayerBasket.Instance.HP <= 0)
        {
            PlayerBasket.Instance.HP = 0;
            animator.SetTrigger("Dead");
            rigid.velocity = Vector2.zero;
            isDead = true;
        }
        else
        {
            StartCoroutine("UnbeatTime");
        }
    }

    IEnumerator UnbeatTime()
    {
        isUnbeat = true;
        for (int i = 0; i < unbeatTime * 10; ++i)
        {
            if (i % 2 == 0)
                spriteRd.color = new Color32(255, 255, 255, 90);
            else
                spriteRd.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);
        }

        spriteRd.color = new Color32(255, 255, 255, 255);
        isUnbeat = false;
        yield return null;
    }


    public void SetDamage(Enemy enemy, float damage) => enemy.Damaged(damage);

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
    
    void InvenActive()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            isPush = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            //isPush = false;
        }

        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerBasket.Instance.invectoryActivated = !PlayerBasket.Instance.invectoryActivated;

            if (PlayerBasket.Instance.invectoryActivated)
            {
                ProjectManager.Instance.inven.invenCanvas.gameObject.SetActive(true);
                StartCoroutine(ProjectManager.Instance.inven.DotweenScroll());
                InvenManager.Instance.indexX = InvenManager.Instance.indexY = 0;
                PlayerBasket.Instance.isInven = true;
            }
        }
    }
}