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

    Coroutine coroutine;

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        way2 = FindObjectOfType<WayPoint2>();
        ed.isDead = false;
        Invoke("Think", 2);
    }
    public abstract void Init();
    void FixedUpdate()
    {
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
        if (Vector3.Distance(transform.position, target.position) < ed.atkRange && ed.state == EnemyState.Idle)
        {
            CancelInvoke("Think");
            spriterenderer.flipX = target.position.x > transform.position.x ? false : true;
            ed.state = EnemyState.Attack;
            coroutine = StartCoroutine(AttackStart(0.2f));
        }
        else if(ed.state == EnemyState.Idle)
        {
            ed.state = EnemyState.Move;

            CancelInvoke("Think");
            Invoke("Think", 1);
            if(coroutine != null)
                StopCoroutine(coroutine);
            anim.SetBool("Attack", false);
        }
    }

    public void Think()
    {
        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;

        anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip.x
        if (nextMove != 0)
            spriterenderer.flipX = nextMove == -3;

        StartCoroutine(SetState(Random.Range(0, 3)));
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


    IEnumerator AttackStart(float delayTime)
    {
        nextMove = 0;
        yield return new WaitForSeconds(delayTime);
        anim.SetBool("Attack", true);
    }

    void DamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ed.hp -= 100;
        }
    }
}
