using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public GameObject gate;
    public GameObject gold;

    void Awake() => Instance = this;

}
