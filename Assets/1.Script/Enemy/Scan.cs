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
            OnDamage();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
    }
    public void OnDamage()
    {
        enemy.ed.anim.SetTrigger("Hit");
        int dirc = transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > 0 ? 3 : -3;
        enemy.ed.rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
    }

}
