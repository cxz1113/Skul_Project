using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FX_Index
{
    Default,
    Slash
}

public abstract class Fx : MonoBehaviour
{
    public FX_Index fxIndex;
    void Start()
    {
        Init();
    }

    public abstract void Init();

    public void EventDest()
    {
        Destroy(gameObject);
    }

}
