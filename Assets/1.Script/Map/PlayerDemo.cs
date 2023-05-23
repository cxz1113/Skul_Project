using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerDemo : MonoBehaviour
{
    float speed = 5;
    public List<Sprite> idle;
    public List<Sprite> attack;
    public PlayerActivity playerActivity;
    public float curHp;
    public float maxHp;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = value;
            PlayerUI.Instance.hpGage.fillAmount = curHp / maxHp;
        }
    }
    /*public float HP
    {
        get { return PlayerData.Instance.nowPlayerData.playerdatajsons[0].curhp; }
        set 
        { 
            PlayerData.Instance.nowPlayerData.playerdatajsons[0].curhp = value;
            PlayerUI.Instance.hpGage.fillAmount = PlayerData.Instance.nowPlayerData.playerdatajsons[0].curhp / PlayerData.Instance.nowPlayerData.playerdatajsons[0].maxhp;
        }
    }*/
    public int head1 = 0;
    public int head2 = 0;
    public int item = 0;
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
            PlayerActivity.Instance.isPush = true;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            PlayerActivity.Instance.isPush = false;
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            HP -= 20;
            Debug.Log(HP);
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
