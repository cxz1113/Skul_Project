using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Player :MonoBehaviour
{
    Enemy enemy;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<Enemy>();
            SetDamage(enemy, player.Damage);
            gameObject.SetActive(false);
        }
    }

    void SetDamage(Enemy enemy, float damage)
    {
        enemy.ed.hp -= damage;
    }
}
