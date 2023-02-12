using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefab;
    public float speed = 5f;
    public Vector3 spawnPoint, odrediste;
    private GameObject instancirani;

    public void Start()
    {
        instancirani = Instantiate(prefab, spawnPoint, Quaternion.identity);
        StartCoroutine(stvoriObjekt());
    }

    public IEnumerator stvoriObjekt()
    {
        while (Vector3.Distance(instancirani.transform.position, odrediste) != 0)
        {
            instancirani.transform.position = Vector3.MoveTowards(instancirani.transform.position, odrediste, speed * Time.deltaTime);
            yield return null;
        }
        instancirani.transform.position = spawnPoint;
        StartCoroutine(stvoriObjekt());
    }
}
