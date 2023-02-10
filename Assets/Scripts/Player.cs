using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	 Rigidbody2D rb;
	 public float speed=10f;
	 public float jumpHeight=10f;
	 private bool naPodu;
	 private int health;
	 public GameObject prefab;
	 public RectTransform canvasRectTransform;
	 public float offset = 50f;
	 private List<GameObject> instantiatedPrefabs = new List<GameObject>();
	 private int score;
	 public TextMeshProUGUI scoreText;
	 public AudioSource audio;
	 private MoveHorizontal script;
	 private float noviSpeed;
	 
	 void UdpateScoreText()
	 {
		 scoreText.SetText("Score: "+score);
	 }


     void Awake()
     {
	     score = 0;
	     rb = GetComponent<Rigidbody2D>();
	     naPodu = true;
	     health = 3;
		 audio.volume = PlayerPrefs.GetFloat("Volume");
		 noviSpeed = speed;
     }

     void Start()
     {
	     StartCoroutine(FixHealth());
     }
     
     // iz nekog razloga UI elemeti se nemogu odma kreirat u Start-u(valjda treba vremena da se canvas ucita)
     private IEnumerator FixHealth()
     {
	     yield return new WaitForSeconds(1);
	     Vector2 lokacijaSrca = new Vector2(100,-100);
	     for (int i = 0; i < 3; i++)
	     {
		     GameObject instantiatedPrefab = Instantiate(prefab, canvasRectTransform,false);
		     instantiatedPrefab.GetComponent<RectTransform>().anchoredPosition = lokacijaSrca;
		     instantiatedPrefabs.Add(instantiatedPrefab);
		     lokacijaSrca.x += offset;
	     }
     }
     

     void OnCollisionEnter2D(Collision2D other)
     {
	     if (other.gameObject.CompareTag("Pod"))
	     {
		     naPodu = true;
	     }
	     else if (other.gameObject.CompareTag("Platforma"))
	     {
		     naPodu = true;
		     script = other.gameObject.GetComponent<MoveHorizontal>();
	     }
     }

     private void OnCollisionStay2D(Collision2D other)
     {
	     if (other.gameObject.CompareTag("Platforma"))
	     {
		     transform.position = Vector3.MoveTowards(transform.position,
			     script.destination+new Vector3(0,transform.position.y-other.transform.position.y,0),
			     script.speed*Time.deltaTime);
	     }
     }

     void OnTriggerEnter2D(Collider2D other)
     {
	     if (other.gameObject.CompareTag("Enemy"))
	     {
		     Destroy(other.gameObject);
		     Destroy(instantiatedPrefabs[health-1].gameObject);
		     health--;
	     }
	     else if (other.gameObject.CompareTag("Bonus"))
	     {
		     score += 500;
		     Destroy(other.gameObject);
		     UdpateScoreText();
	     }
     }


     void Update()
     {
	     
	     if (!naPodu)
	     {
		     noviSpeed = speed / 3;
	     }
         // ljevo/desno kretanje
         if (Input.GetKey(KeyCode.LeftArrow))
         {
	         rb.AddForce(new Vector2(-1,0) * noviSpeed);
         }
         else if (Input.GetKey(KeyCode.RightArrow))
         {
			 rb.AddForce(new Vector2(1,0) * noviSpeed);
         }
         // skakanje
         if(Input.GetKey(KeyCode.Space) && naPodu){
			 rb.AddForce(new Vector2(0,1)*jumpHeight,ForceMode2D.Impulse);
			 naPodu = false;
         }
     }
}

