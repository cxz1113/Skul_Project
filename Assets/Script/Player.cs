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

    Rigidbody2D rigid;
    Animator animator;

    int dashCount = 0;
    int maxDashCount = 2;
    float speed = 7f;
    float dashSpeed = 30f;
    float dashDis = 1.5f;
    float dashDelay = 0;
    float dashCoolTime = 1.2f;
    float jumpPower = 9f;
    bool isGround = true;
    bool jumped = false;
    bool isDashCool = false;


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

        if (Input.GetKeyDown(KeyCode.Z) && !isDashCool)
        {
            StartCoroutine("StartDash");
        }
    }

    void LookDir()
    {
        switch (playerDir)
        {
            case PlayerDir.right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case PlayerDir.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
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
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * (x * Time.deltaTime * speed));
        // transform.Translate(new Vector2(x * Time.deltaTime * speed, 0));

        animator.SetBool("Walk", true);
        if (x > 0)
        {
            playerDir = PlayerDir.right;
        }
        else if (x < 0)
        {
            playerDir = PlayerDir.left;

        }
        else
        {
            animator.SetBool("Walk", false);
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

        rigid.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator StartDash()
    {
        if (dashCount < maxDashCount)
        {
            StartCoroutine("Dash");
            dashCount++;
        }
        
        if(dashCount >= maxDashCount)
        {
            isDashCool = true;  
            dashDelay = dashCoolTime;
            while (dashDelay > 0)
            {
                dashDelay -= Time.deltaTime;
                Debug.Log(dashDelay);
                yield return new WaitForFixedUpdate();
            }
            dashCount = 0;
            isDashCool = false;
        } 
    }

    IEnumerator Dash()
    {
        float dashed = 0;
        int dir;
        dir = playerDir == PlayerDir.right ? 1 : -1;

        animator.SetBool("Dash", true);

        while (dashed < dashDis)
        {
            float deltaDash = Time.deltaTime * dashSpeed;
            transform.Translate(transform.right * (dir * deltaDash));
            dashed += deltaDash;
            yield return new WaitForFixedUpdate();
        }
        
        animator.SetBool("Dash", false);
    }

    IEnumerator CoolDown(float coolTime)
    {
        float delay = coolTime;

        while (delay > 0)
        {
            delay -= Time.deltaTime;
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