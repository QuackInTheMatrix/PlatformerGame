using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratiPlayera : MonoBehaviour
{
    public GameObject player;
    public float offset = 4f;
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, offset, -10);
    }
}
