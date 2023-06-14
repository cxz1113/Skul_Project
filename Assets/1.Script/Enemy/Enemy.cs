using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public EnemyType type;
    public EnemyState state;
    public float rayY;
    public float maxhp;
    public float hp;
    public float damage;
    public float atkRange;
    public float atkDelay;

    public bool isDead;
}

public enum EnemyType
{
    Tanker,
    Acher,
    knigt
}

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hit,
    Dead
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyData ed = new EnemyData();

    public Rigidbody2D rigid;
    public CapsuleCollider2D capsuleColl;
    public Animator anim;
    public SpriteRenderer spriterenderer;
    public Transform target;
    public WayPoint2 way2;

    public int nextMove;
    private bool canThink = true;
    private bool canAttack = true;

    Coroutine coroutine;

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        way2 = FindObjectOfType<WayPoint2>();
        ed.isDead = false;
        Think();

        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
    }
    public abstract void Init();
    void FixedUpdate()
    {
        if (ed.state == EnemyState.Hit || ed.state == EnemyState.Dead)
            return;

        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        PlatformCheck();
    }

    void Update()
    {
        if (ed.state == EnemyState.Dead)
            return;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), target.gameObject.GetComponent<CapsuleCollider2D>());
            return;
        }

        if (ed.isDead == true || ed.hp <= 0)
        {
            StopAllCoroutines();
            //nextMove = 0;
            anim.SetBool("Dead", true);
            ed.state = EnemyState.Dead;

            Invoke("Die", 2f);
        }
        DamageTest();

        if (ed.state == EnemyState.Attack)
            return;

        // 공격 거리 체크
        if (Vector3.Distance(transform.position, target.position) < ed.atkRange)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            spriterenderer.flipX = target.position.x > transform.position.x ? false : true;

            if (canAttack)
                AttackStart();
            else
            {
                nextMove = spriterenderer.flipX ? -3 : 3;
                anim.SetInteger("Walk", nextMove);
            }
        }
        else if (canThink && ed.state != EnemyState.Attack)
        {
            StartCoroutine("Think");
        }
    }

    public IEnumerator Think()
    {
        canThink = false;

        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;
        ed.state = nextMove == 0 ? EnemyState.Idle : EnemyState.Move;
        anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip.x
        if (nextMove != 0)
            spriterenderer.flipX = nextMove == -3;

        yield return new WaitForSeconds(Random.Range(1, 3));

        canThink = true;
    }

    public void Turn()
    {
        nextMove *= -1;
        spriterenderer.flipX = nextMove == -3;

        CancelInvoke();

        Invoke("Think", 2);
    }

    void AttackStart()
    {
        StartCoroutine("AttackCoolDown");
        nextMove = 0;
        anim.SetTrigger("Attack");
        ed.state = EnemyState.Attack;
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(ed.atkDelay);
        canAttack = true;
    }

    //공격 끝나는 시점, hit 끝나는 시점
    void EventAttackEnd()
    {
        ed.state = EnemyState.Idle;
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Hit");
        StartCoroutine("Think");
    }

    public void Damaged(float damage)
    {
        ed.hp -= damage;
        if(ed.state != EnemyState.Attack && ed.state != EnemyState.Dead)
        {
            anim.SetTrigger("Hit");
            ed.state = EnemyState.Hit;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            int dir = spriterenderer.flipX ? -1 : 1;
            rigid.AddForce(new Vector2(dir, 1) * 5 * rigid.mass, ForceMode2D.Impulse);
        }

        nextMove = 0;
    }

    void Die()
    {
        Destroy(gameObject);
        if(way2 !=null)
            way2.killcount++;
    }

    void DamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ed.hp -= 100;
        }
    }

    void PlatformCheck()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y - ed.rayY);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
            Turn();
    }
}
