using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject endCredits;
    public GameObject kapaPrefab;
    public AudioSource audio;
    public Vector3 odrediste, pozicija;

    void Start()
    {
        audio.volume = PlayerPrefs.GetFloat("Volume");
        odrediste=endCredits.GetComponent<RectTransform>().localPosition+new Vector3(0, 3000, 0);
        StartCoroutine(MoveCredits());
        StartCoroutine(BacajKape());
        pozicija = new Vector3(kapaPrefab.transform.position.x, kapaPrefab.transform.position.y, 0);
    }
    
    private IEnumerator BacajKape()
    {
        while (true)
        {
            GameObject instancirani = Instantiate(kapaPrefab, pozicija + new Vector3(Random.value * 100, 0, 0),Quaternion.identity);
            instancirani.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            yield return null;
        }
    }

    private IEnumerator MoveCredits()
    {
        while (Vector3.Distance(endCredits.GetComponent<RectTransform>().localPosition, odrediste) != 0)
        {
            endCredits.GetComponent<RectTransform>().localPosition= Vector3.MoveTowards(endCredits.GetComponent<RectTransform>().localPosition, odrediste, 100f * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }
}
