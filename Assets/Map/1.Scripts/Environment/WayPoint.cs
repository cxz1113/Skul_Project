using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : Environment
{
    public override void Initialize()
    {
        evd.obj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        
    }
}
