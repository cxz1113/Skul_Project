using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;

    Enemy enemy;

    public bool hit;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        hit = false;
    }
    void Update()
    {
        tans.position = EnemyObj.transform.position;
    }

    void OnTriggerEnter2D(Collider2D hitcollision)
    {
        hit = true;
        if (hitcollision.gameObject.tag == "Player")
        {
            OnDamage();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
    }
    public void OnDamage()
    {
        enemy.ed.anim.SetTrigger("Hit");
        int dirc = transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > 0 ? 1 : -1;
        enemy.ed.rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
    }

}
