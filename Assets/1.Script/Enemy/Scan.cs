using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;

    Enemy enemy;

    public bool scanP;
    void Update()
    {
        tans.position = EnemyObj.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scanP = true;
            enemy.ed.spriterenderer.flipX = enemy.ed.target.position.x < transform.position.x ? false : true;
            Invoke("AttackStart", 0);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke("AttackStart");
            enemy.ed.anim.SetBool("Attack", false);
            enemy.Think();
        }
    }

    void AttackStart()
    {
        enemy.nextMove = 0;
        enemy.ed.anim.SetBool("Attack", true);
    }
}
