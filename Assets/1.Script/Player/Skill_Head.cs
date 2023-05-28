using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Head : MonoBehaviour
{
    public float coolTime;
    public int dir;

    float originalGravity;
    float speed = 40f;
    bool isFlying = true;
    Rigidbody2D rigid;
    public Player_LittleBorn player;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어와 충돌 방지
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CapsuleCollider2D>());
        rigid = GetComponent<Rigidbody2D>();
        originalGravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        Invoke("OffFlying", 1f);
        Invoke("Dest", coolTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
            rigid.velocity = new Vector2(dir * speed, 0);
    }

    IEnumerator OffFlying()
    {
        isFlying = false;
        rigid.gravityScale = originalGravity;
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CapsuleCollider2D>(), false);
        gameObject.layer = 0;
    }

    void Dest()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            StartCoroutine(OffFlying());
        else if (!isFlying)
        {
            player.ResetCool();
            //animators[0] = 리틀본 애니메이션
            player.animator.runtimeAnimatorController = player.animators[0];
            Dest();
        }
    }
}
