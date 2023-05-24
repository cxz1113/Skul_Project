using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerMove : MonoBehaviour
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
        //움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //바닥 체크
        Vector2 frontVec = new Vector2(rigid.position.x - 0.5f + nextMove * 0.75f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
            Turn();

    }

    //몬스터 움직임 AI
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
        float nextMoveTime = Random.Range(0, 5f);
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
}
