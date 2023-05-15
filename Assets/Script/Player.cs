using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum PlayerDir
    {
        left,
        right
    }

    PlayerDir playerDir = PlayerDir.right;

    [SerializeField] private Rigidbody2D rigid;

    float speed = 7f;
    float dashSpeed = 30f;
    float jumpPower = 9f;
    float dashDis = 1.5f;
    bool isGround = true;
    bool jumped = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.C))
            Jump();

        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine("Dash");
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(x * Time.deltaTime * speed, 0));
        if (x > 0)
            playerDir = PlayerDir.right;
        else if (x < 0)
            playerDir = PlayerDir.left;
    }

    void Jump()
    {
        if (!isGround)
        {
            if (jumped)
                return;
            else
            {
                jumped = true;
            }
        }

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        float dashed = 0;
        int x;
        if (playerDir == PlayerDir.right)
            x = 1;
        else
            x = -1;

        while (dashed < dashDis)
        {
            float temp = Time.deltaTime * dashSpeed;
            transform.Translate(new Vector2(x * temp, 0));
            dashed += temp;
            yield return new WaitForFixedUpdate();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            jumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
