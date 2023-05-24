using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x -0.5f + nextMove * 1, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();


    }

    void Think()
    {
        //다음 움직임 방향 * 속도
        nextMove = Random.Range(-1, 2) * 3;

        //스프라이트 애니메이션
        anim.SetInteger("Walk", nextMove);
        if (nextMove == 0)
            anim.SetInteger("Idle", nextMove);

        //스프라이트 Flip
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //다음움직임 시간셋팅 + 재귀함수
        float nextMoveTime = Random.Range(0, 5f);
        Invoke("Think", nextMoveTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
