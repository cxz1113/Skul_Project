using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnvironmentData
{
    public GameObject obj;
}
public abstract class Environment : MonoBehaviour
{
    public EnvironmentData evd = new EnvironmentData();
    [SerializeField] public List<Sprite> active;

    public abstract void Initialize();

    // Update is called once per frame
    void Update()
    {

    }
}
