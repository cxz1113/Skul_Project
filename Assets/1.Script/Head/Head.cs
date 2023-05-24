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
    public int attack;
}
public abstract class Head : MonoBehaviour
{
    public SkulStatus ss = new SkulStatus();
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> skillSprites = new List<Sprite>();
    public Player player;
    public SkulData skuljson;
    public abstract void Init();
}
