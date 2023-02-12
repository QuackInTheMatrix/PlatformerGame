using System.Collections;
using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    public float yMin, yMax;
    public Vector3 positionMin, positionMax, destination;
    public bool moveUp;
    public float elapsedTime;
    public float speed= 5f;
    void Start()
    {
        moveUp = false;
        positionMin = new Vector3(transform.position.x, yMin, transform.position.z);
        positionMax = new Vector3(transform.position.x,yMax, transform.position.z);
        destination = positionMin;
        StartCoroutine(moveObject());
    }

    public IEnumerator moveObject()
    {
        while (true)
        {
            while (Vector3.Distance(transform.position, destination) != 0)
            {
                elapsedTime = Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * elapsedTime);
                yield return null;
            }
            moveUp = !moveUp;
            if (moveUp)
            {
                destination = positionMax;
            }
            else
            {
                destination = positionMin;
            }
        }
    }
}
