using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Head : MonoBehaviour
{
    public float coolTime;
    public int dir;

    float speed = 20f;
    bool isFlying = true;
    Rigidbody2D rigid;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("OffFlying", 1f);
        Invoke("Dest", coolTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
            rigid.velocity = new Vector2(dir * speed, 0);
    }

    void OffFlying()
    {
        isFlying = false;
        rigid.gravityScale = 3;
        rigid.velocity = Vector2.zero;
    }

    void Dest()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            OffFlying();
        else if (!isFlying)
        {
            player.ResetCool();
            Dest();
        }
    }
}
