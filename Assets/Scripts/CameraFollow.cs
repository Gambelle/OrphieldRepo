using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMin, xMax, yMin, yMax;
    public float followSpeed = 2f;
    public float offset = 2f;
    public Transform target;

    private float posY, posX;


    // Update is called once per frame
    void Update()
    {
        if (target.position.y + offset < yMin)
            posY = yMin;
        else if(target.position.y + offset > yMax)
            posY = yMax;
        else
            posY = target.position.y + offset;

        if (target.position.x < xMin)
            posX = xMin;
        else if (target.position.x > xMax)
            posX = xMax;
        else
            posX = target.position.x;


        Vector3 newPos = new Vector3(posX, posY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
