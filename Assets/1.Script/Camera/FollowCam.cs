using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Test Code
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = FindObjectOfType<Player>().transform;
        if (target==null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        Vector3 pos = target.position;
        pos.z = -10;
        pos.y += 2;
        transform.position = Vector3.Lerp(transform.position, pos, 10f);
    }
}
