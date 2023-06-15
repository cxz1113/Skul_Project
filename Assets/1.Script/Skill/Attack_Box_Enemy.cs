using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Enemy : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player = collision.gameObject;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player = null;

    }
}
