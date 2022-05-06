using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1.0f;

    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        //transform.position = new Vector2(0.1f, 0.1f);

        GameObject capsule = GameObject.Find("Player");

        target = capsule.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float step = 1 * speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        //if (Vector2.Distance(transform.position, target.position) < 0.001f)
        //{
        //    // Swap the position of the cylinder.
        //    target.position *= -1.0f;
        //}
    }
}
