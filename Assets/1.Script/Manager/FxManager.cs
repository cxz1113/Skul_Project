using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance;

    public List<Fx> fxs = new List<Fx>();
    public Fx fx_tp;
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

    public Fx FxByPlayer()
    {
        switch (player.stpd.skul)
        {
            case PlayerSkul.LittleBorn:
                return fxs[0];

            case PlayerSkul.Sword:
            case PlayerSkul.Wolf:
                return fxs[1];

            default:
                return null;
        }
    }
    public void CreateFx_Effect_Tp(Transform trans )
    {
        Vector3 pos = trans.position + Vector3.up;
        Instantiate(fx_tp, pos, Quaternion.identity, transform);
    }
    public void CreateFx_Effect_Tp(Transform trans, float col_sizeX, float col_sizeY , float times)
    {
        Vector3 pos = trans.position + Vector3.up * col_sizeY / 2;
        Fx tp_Effect = Instantiate(fx_tp, pos, Quaternion.identity, transform);
        tp_Effect.transform.localScale *= (col_sizeX + col_sizeY) / 2 / 3 * times;
    }
}
