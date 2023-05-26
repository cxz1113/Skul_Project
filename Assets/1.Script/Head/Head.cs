using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkulStatus
{
    public Sprite headStatus1;
    public Sprite headStatus2;
    public Sprite headInven;
    public Sprite headItem;
    public Sprite skill1;
    public Sprite Skill2;
    public SkulData json;
    public int attack;
}
public abstract class Head : MonoBehaviour
{
    public SkulStatus ss = new SkulStatus();
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> skillSprites = new List<Sprite>();
    public Player player;
    public SkulData skuljson;
    public bool isHead { get; set; }
    public abstract void Init();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            ProjectManager.Instance.heads[1] = this;
            DataManager.Instance.playerData.nowPlayerData.playerdatajsons[0].head2 = gameObject.name;
            transform.gameObject.SetActive(false);
        }
    }
}
