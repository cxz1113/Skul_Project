using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;
    [SerializeField] Enemy enemy;

    void Update()
    {
        tans.position = EnemyObj.transform.position;
    }

    void OnTriggerEnter2D(Collider2D hitcollision)
    {
        if (hitcollision.gameObject.tag == "Player")
        {
            if (enemy.ed.entype != EnemyType.Tanker)
            {
                OnDamage();
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
    }

    public void OnDamage()
    {
        //enemy.ed.hp -= Player.Damage;
        enemy.ed.anim.SetTrigger("Hit");
        int dirc = transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > 0 ? 1 : -1;
        enemy.ed.rigid.AddForce(new Vector2(dirc, 3) * 1 , ForceMode2D.Impulse);
        if (enemy.ed.hp <= 0)
        {
            enemy.isDead = true;
        }
    }

}
