using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    float speed = 5;
    public List<Sprite> idle;
    public List<Sprite> attack;

    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(idle, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<SpriteAnimation>().SetSprite(attack, 0.2f,Idle);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            MapManager.Instance.isPush = true;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            MapManager.Instance.isPush = false;
        }

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 vec = new Vector2(x, 0) * Time.deltaTime * speed;
        transform.Translate(vec);
    }
    void Idle()
    {
        GetComponent<SpriteAnimation>().SetSprite(idle, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            MapManager.Instance.gold.gameObject.SetActive(true);
        }

    }
}
