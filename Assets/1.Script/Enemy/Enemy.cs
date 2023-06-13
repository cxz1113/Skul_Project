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
    public float destroyTime;
    private bool isAttack = false;
    private bool canThink = true;

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
        if (ed.state == EnemyState.Hit || ed.state == EnemyState.Attack)
            return;
        //Move
        if(rigid.bodyType == RigidbodyType2D.Dynamic)
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y - ed.rayY);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }

    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
            return;
        }

        if (ed.isDead == true || ed.hp <= 0)
        {
            destroyTime += Time.deltaTime;
            CancelInvoke("Think");
            if (coroutine != null)
                StopCoroutine(coroutine);
            
            nextMove = 0;

            anim.ResetTrigger("Hit");
            anim.SetBool("Dead", true);
            if (destroyTime >= 2)
            {
                //way2.killCount++;
                Destroy(gameObject);
            }
        }
        DamageTest();

        // 공격 거리 체크
        if (Vector3.Distance(transform.position, target.position) < ed.atkRange)
        {
            nextMove = 0;
            spriterenderer.flipX = target.position.x > transform.position.x ? false : true;
            ed.state = EnemyState.Attack;
            coroutine = StartCoroutine(AttackStart(0));
        }
        /*
        else if(ed.state == EnemyState.Idle)
        {
            ed.state = EnemyState.Move;
            CancelInvoke("Think");
            Invoke("Think", 1);
            Think();
            if(coroutine != null)
                StopCoroutine(coroutine);
        }*/
        else if (canThink && ed.state != EnemyState.Attack)
        {
            StartCoroutine("Think");
        }

    }

    public IEnumerator Think()
    {
        Debug.Log("Think");
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

    IEnumerator SetState(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ed.state = EnemyState.Idle;
        Think();
    }

    public void Turn()
    {
        nextMove *= -1;
        spriterenderer.flipX = nextMove == -3;

        CancelInvoke();

        Invoke("Think", 2);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.bodyType = RigidbodyType2D.Static;
            capsuleColl.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            capsuleColl.isTrigger = false;
        }
    }
    */

    IEnumerator AttackStart(float delayTime)
    {
        nextMove = 0;
        yield return new WaitForSeconds(delayTime);
        anim.SetTrigger("Attack");
        isAttack = true;
    }

    void EventAttackEnd()
    {
        isAttack = false;
        ed.state = EnemyState.Idle;
        anim.ResetTrigger("Attack");
    }

    void DamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ed.hp -= 100;
        }
    }
}
