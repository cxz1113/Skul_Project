using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;
    [SerializeField] Animator animator;
    [SerializeField] CheckCollider checkCollider;
    [SerializeField] bool isDelay;
    [SerializeField] int pattern;
    float invokeDelay;
    float fireDelay = 2f;
    bool active = false;

    void Start()
    {
        invokeDelay = isDelay ? fireDelay / 2 : 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        FIre();
    }

    public void FIre()
    {
        if (pattern != 0 && pattern != 1)
            return;

        switch (pattern)
        {
            case 0:
                if (checkCollider.active)
                {
                    InvokeRepeating("SetAnimator", 0, fireDelay);
                    active = true;
                }
                break;
            case 1:
                if (checkCollider.active)
                {
                    InvokeRepeating("SetAnimator", invokeDelay, fireDelay);
                    active = true;
                }
                break;
        }

        
    }
    
    void SetAnimator()
    {
        animator.SetTrigger("Attack");
    }

    //공격 - 6번째 스프라이트(발사)
    void EvnetFire()
    {
        Instantiate(prefab, parent);
    }
}
