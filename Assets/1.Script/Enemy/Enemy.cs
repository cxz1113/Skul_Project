using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public EnemyType entype;
    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer spriterenderer;
    public Transform target;
    public float rayY;

    public float maxhp;
    public float hp;
    public float damage;
}

public enum EnemyType
{
    Tanker,
    Acher,
    knigt
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyData ed = new EnemyData();
   
    public int nextMove;
    public bool isDead;
    public float DestroyTime;

    void Awake()
    {
        ed.rigid = GetComponent<Rigidbody2D>();
        ed.spriterenderer = GetComponent<SpriteRenderer>();
        ed.anim = GetComponent<Animator>();
        ed.target = GameObject.FindWithTag("Player").transform;
        isDead = false;
        Invoke("Think", 2);
    }
    public abstract void Init();
    void FixedUpdate()
    {
        //Move
        ed.rigid.velocity = new Vector2(nextMove, ed.rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(ed.rigid.position.x + nextMove * 0.75f, ed.rigid.position.y - ed.rayY);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }

    }

    void Update()
    {
        if (isDead == true || ed.hp <= 0)
        {
            DestroyTime += Time.deltaTime;
            CancelInvoke("Think");
            CancelInvoke("AttackStart");
            nextMove = 0;
            ed.anim.SetBool("Dead", true);
            if (DestroyTime == 1)
            {
                Destroy(gameObject);
            }
        }
        DamageTest();
    }

    public void Think()
    {
        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;

        ed.anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip.x
        if (nextMove != 0)
            ed.spriterenderer.flipX = nextMove == -3;

        float nextMoveTime = Random.Range(0, 3);
        Invoke("Think", nextMoveTime);
    }

    public void Turn()
    {
        nextMove *= -1;
        ed.spriterenderer.flipX = nextMove == -3;

        CancelInvoke();
        Invoke("Think", 2);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke("Think");
            ed.spriterenderer.flipX = ed.target.position.x > transform.position.x ? false : true;
            Invoke("AttackStart", 0);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke("Think");
            Invoke("Think",1);
            CancelInvoke("AttackStart");
            ed.anim.SetBool("Attack", false);
        }
    }
   
    void AttackStart()
    {
        nextMove = 0;
        ed.anim.SetBool("Attack", true);
    }

    void DamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ed.hp -= 100;
        }
    }
}
