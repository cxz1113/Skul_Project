using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint1 : Environment
{
    BoxCollider2D collider2D;
    public override void Initialize()
    {
        //evd.obj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!MapManager.Instance.isActive && MapManager.Instance.isWay)
        {
            MapManager.Instance.isWay = false;
            GetComponent<SpriteAnimation>().SetSprite(active, 0.2f);
            collider2D.enabled = true;
        }
    }
}
