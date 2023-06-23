using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FX_Index
{
    LittBorn,
    Wolf,
    Sword
}

public abstract class Fx : MonoBehaviour
{
    public Player player;
    public FX_Index fxIndex;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        player = FindObjectOfType<Player>();
    }

    public abstract void Init();
}
