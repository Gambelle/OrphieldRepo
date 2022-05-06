using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    /*void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 150f, rb.velocity.y);
        if(Input.GetButtonDown("Jump"))
        {
           rb.velocity = new Vector3(rb.velocity.x, 200f);
        }
        
    }*/
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * 5f);
        
    }
}
