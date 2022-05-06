using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite forwardSprite;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        if (rb.velocity.x > 0.1)
            spriteRenderer.sprite = rightSprite;
        else if (rb.velocity.x < -0.1)
            spriteRenderer.sprite = leftSprite;
        else
            spriteRenderer.sprite = forwardSprite;
    }
 }
