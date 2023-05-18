using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skul : MonoBehaviour
{
    public static Skul Instance;

    private void Awake() => Instance = this;


    public struct Skul_Data
    {
        public float hp;
        public float speed;
    }

    public abstract void Attack();
    public abstract void Skill_1();
    public abstract void Skill_2();
    public abstract void Ability();
    
}
