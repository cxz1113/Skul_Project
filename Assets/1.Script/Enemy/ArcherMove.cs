using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMove : MonoBehaviour
{
    [SerializeField] Scan scan;
    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Transform target;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 2);
    }
    void Update()
    {
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.75f, rigid.position.y - 1.2f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null)
            Turn();

    }
    // ���� ������ AI
    public void Think()
    {
        //���� ������ ���� * �ӵ�
        nextMove = Random.Range(-1, 2) * 3;

        //��������Ʈ �ִϸ��̼�
        anim.SetInteger("Walk", nextMove);

        //��������Ʈ Flip
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -3;

        //���������� �ð����� + ����Լ�
        float nextMoveTime = Random.Range(0, 3);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject.transform;
        if (collision.gameObject.tag == "Player")
        {
            OnDamage(collision.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

    }

    void OnDamage(Vector2 targetPos)
    {
        anim.SetBool("Hit", true);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc + 5, 1) * 7, ForceMode2D.Impulse);
    }
}
