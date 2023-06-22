using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private void Start() => Invoke("Dest", 2f);

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 3 * Time.deltaTime);
    }

    void Dest()
    {
        Destroy(gameObject);
    }
}
