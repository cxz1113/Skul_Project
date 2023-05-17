using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    private List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer sr;

    private float spriteDelayTime;
    private float delayTime = 0;
    int spriteIndex = 0;
    UnityAction action;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0)
            return;

        delayTime += Time.deltaTime;
        if(delayTime > spriteDelayTime)
        {
            delayTime = 0;
            sr.sprite = sprites[spriteIndex];
            spriteIndex++;
            if(spriteIndex > sprites.Count - 1)
            {
                spriteIndex = 0;

                if (action != null)
                {
                    sprites.Clear();
                    action();
                    action = null;
                }
            }
        }
    }

    void Init()
    {
        delayTime = 0f;
        sprites.Clear();
        spriteIndex = 0;
    }

    public void SetSprite(List<Sprite> argSprite, float delay)
    {
        Init();
        sprites = argSprite.ToList();
        spriteDelayTime = delay;
    }
    public void SetSprite(List<Sprite> argSprite, float delay,UnityAction action)
    {
        Init();
        this.action = action;
        sprites = argSprite.ToList();
        spriteDelayTime = delay;
    }
}
