using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    float speed = 5;
    public List<Sprite> idle;
    public List<Sprite> attack;
    // Start is called before the first frame update
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

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 vec = new Vector2(x, 0) * Time.deltaTime * 5;
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
