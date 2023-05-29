using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CheckCollider checkCollider;
    public int pattern;
    float fireDelay = 2f;
    float delayTime = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayTime += Time.deltaTime;
        if (delayTime >= fireDelay && checkCollider.active)  
        {
            FIre();
            delayTime = 0;
        }
    }

    public void FIre()
    {
        animator.SetTrigger("Attack");
    }
    
}
