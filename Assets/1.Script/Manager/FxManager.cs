using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance;

    public List<GameObject> fxs = new List<GameObject>();
    Player player;

    void Start()
    {
        Instance = this;
        //FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            FindPlayer();
    }

    void FindPlayer()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public GameObject FxByPlayer()
    {
        switch (player.stpd.skul)
        {
            case PlayerSkul.LittleBorn:
            case PlayerSkul.Wolf:
                return fxs[0];

            case PlayerSkul.Sword:
                return fxs[1];

            default:
                return null;
        }
    }
}
