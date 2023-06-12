using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Player :MonoBehaviour
{
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
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
        if (collision.CompareTag("Enemy") && !enemies.Contains(collision.GetComponent<Enemy>()))
            enemies.Add(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            enemies.Remove(collision.GetComponent<Enemy>());
    }
}
