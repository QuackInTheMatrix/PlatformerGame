using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float xMin, xMax, speed;
    public Vector3 positionMin, positionMax, destination;
    public bool moveRight, immune;
    public float elapsedTime;
    void Start()
    {
        moveRight = false;
        speed = 5f;
        positionMin = new Vector3(xMin, transform.position.y, transform.position.z);
        positionMax = new Vector3(xMax, transform.position.y, transform.position.z);
        destination = positionMin;
        StartCoroutine(MoveObject());
    }

    public IEnumerator MoveObject()
    {
        while (true)
        {
            while (Vector3.Distance(transform.position, destination) != 0)
            {
                elapsedTime = Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * elapsedTime);
                yield return null;
            }
            moveRight = !moveRight;
            if (moveRight)
            {
                destination = positionMax;
                transform.Rotate(0,0,-20);
            }
            else
            {
                destination = positionMin;
                transform.Rotate(0,0,20);
            }
        }
    }
}
