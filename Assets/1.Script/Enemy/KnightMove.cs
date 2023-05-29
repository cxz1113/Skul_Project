using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Transform target;

    public bool scan;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 2);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y -1.2f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
            Turn();
       
    }
    // 몬스터 움직임 AI
    void Think()
    {
        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;

        //스프라이트 애니메이션
        anim.SetInteger("Walk", nextMove);

        //스프라이트 Flip
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -3;

        //다음움직임 시간셋팅 + 재귀함수
        float nextMoveTime = Random.Range(0,3);
        Invoke("Think", nextMoveTime);
    }

    //위치를 반대로 돌리는 함수
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -3;

        CancelInvoke();
        Invoke("Think", 2);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject.transform;
        if (scan == true)
        {
            CancelInvoke("Think");
            if (target.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (target.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            Invoke("AttackStart", 0);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (scan == false)
        {
            CancelInvoke("AttackStart");
            anim.SetBool("Attack", false);
            Think();
        }
    }

    void OnDamage(Vector2 targetPos)
    {
        anim.SetBool("Hit", true);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) *7, ForceMode2D.Impulse);
    }

    void AttackStart()
    {
        nextMove = 0;
        anim.SetBool("Attack", true);
    }
}
