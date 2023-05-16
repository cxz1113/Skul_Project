using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Environment
{
    public override void Initialize()
    {
        evd.obj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GetComponent<SpriteAnimation>().SetSprite(active, 0.05f);
    }
}
