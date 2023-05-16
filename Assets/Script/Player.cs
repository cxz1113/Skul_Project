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
    PlayerDir playerDir_past;

    Rigidbody2D rigid;
    Animator animator;
    
    float moveSpeed = 7f;

    int dashCount = 1;
    int maxDashCount = 2;
    float dashPower = 15f;
    float dashDelay = float.MaxValue;
    float dashCoolTime = 0.8f;

    float jumpPower = 12f;
    bool isGround = true;
    bool jumped = false;

    [SerializeField] float tempds = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookDir();
        JumpAnimation();
        if (Input.GetKeyDown(KeyCode.C))
            Jump();

        if (Input.GetKey(KeyCode.Z))
        {
            Attack();
        }


        dashDelay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (animator.GetBool("Dash"))
            {
                if (dashCount < maxDashCount)
                {
                    StopDash();
                    Dash();
                    animator.Play("Player Dash",-1,0);
                    dashCount++;
                }
            }
            else if (dashDelay > dashCoolTime)
            {
                Dash();
                dashDelay = 0f;
            }
        }
    }

    void LookDir()
    {
        switch (playerDir)
        {
            case PlayerDir.right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (playerDir_past != playerDir)
                {
                    transform.Translate(Vector2.right * 1.75f);
                    //transform.Translate(Vector2.right * tempds * Time.deltaTime); 
                }
                break;

            case PlayerDir.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (playerDir_past != playerDir)
                {
                    transform.Translate(Vector2.right * 1.75f);
                    //transform.Translate(Vector2.right * tempds * Time.deltaTime);

                    //transform.position += transform.TransformVector(Vector3.right) * 1.75f * 2;
                }
                break;
        }
        playerDir_past = playerDir;
    }

    void JumpAnimation()
    {
        if (rigid.velocity.y > 0.05f)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }
        else if (rigid.velocity.y < -0.05f)
        {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }

    void Move()
    {
        if (animator.GetBool("Dash"))
            return;

        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * (x * Time.deltaTime * moveSpeed));
        // transform.Translate(new Vector2(x * Time.deltaTime * speed, 0));

        animator.SetBool("Walk", true);

        if(x == 0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            playerDir = x > 0 ? PlayerDir.right : playerDir = PlayerDir.left;
        }
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

        StopDash();
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 3f;
        animator.SetBool("Dash", false);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }

    void Dash()
    {
        animator.SetBool("Dash", true);
        rigid.gravityScale = 0;
        rigid.velocity = new Vector2(rigid.velocity.x, 0);


        rigid.AddForce(transform.right * dashPower, ForceMode2D.Impulse);
    }

    void StopDash()
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 3f;
        animator.SetBool("Dash", false);
        dashCount = 1;
    }

    void Attack()
    {

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