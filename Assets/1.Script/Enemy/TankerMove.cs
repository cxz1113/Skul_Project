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
        //������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //�ٴ� üũ
        Vector2 frontVec = new Vector2(rigid.position.x - 0.5f + nextMove * 0.75f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
            Turn();

    }

    //���� ������ AI
    void Think()
    {
        //���� ������ ���� * �ӵ�
        nextMove = Random.Range(-1, 2) * 3;

        //��������Ʈ �ִϸ��̼�
        anim.SetInteger("Walk", nextMove);

        //��������Ʈ Flip
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -3;

        //���������� �ð����� + ����Լ�
        float nextMoveTime = Random.Range(0, 5f);
        Invoke("Think", nextMoveTime);
    }

    //��ġ�� �ݴ�� ������ �Լ�
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -3;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
