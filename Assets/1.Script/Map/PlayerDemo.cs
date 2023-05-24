using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerDemo : MonoBehaviour
{
    public Head head;
    public List<Head> heads = new List<Head>();
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
            ProjectManager.Instance.ui.hpGage.fillAmount = curHp / maxHp;
            ProjectManager.Instance.ui.curHpTxt.text = string.Format("{0}", curHp);
        }
    }

    public int head1 = 0;
    public int head2 = 0;
    public int item = 0;
    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(idle, 0.2f);
        head = heads[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (heads.Count > 2)
            return;
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
            DataManager.Instance.playerData.nowPlayerData.playerdatajsons[0].curhp = curHp;
            Debug.Log(HP);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            HP += 20;
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
