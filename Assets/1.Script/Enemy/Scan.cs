using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;

    [SerializeField] KnightMove knight;

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
            knight.spriteRenderer.flipX = knight.target.position.x > transform.position.x ? false : true;
            Invoke("AttackStart", 0);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("AttackStart");
        knight.anim.SetBool("Attack", false);
        knight.Think();
    }

    void AttackStart()
    {
        knight.nextMove = 0;
        knight.anim.SetBool("Attack", true);
    }
}
