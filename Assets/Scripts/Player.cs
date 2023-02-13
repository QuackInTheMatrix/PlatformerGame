using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;

public class Player : MonoBehaviour {
	
	 Rigidbody2D rb;
	 public float speed=10f;
	 public float jumpHeight=10f;
	 private bool naPodu;
	 public GameObject prefab, diplomaPrefab, gameOverButton, gameOverText;
	 public RectTransform canvasRectTransform;
	 public float offset = 50f;
	 private List<GameObject> instanciraniHealth = new List<GameObject>();
	 private List<GameObject> instanciraneDiplome = new List<GameObject>();
	 public List<GameObject> gameOverObjekti = new List<GameObject>();
	 private int score, health;
	 public AudioSource audio;
	 private MoveHorizontal script;
	 private float noviSpeed;
	 private bool immune;
	 public Vector3 startPosition;
	 private bool gameOver;
	 private Color defaultColor;
	 
	 void UdpateScore()
	 {
		 Vector2 lokacijaDiplome = new Vector2(-50, -150);
		 if (instanciraneDiplome.Count!=0)
		 {
			 if (instanciraneDiplome.Count!=score)
			 {
				 PlayerPrefs.SetInt("score",score);
			 }
			 foreach (GameObject diplomaPrefab in instanciraneDiplome)
			 {
				 Destroy(diplomaPrefab);
			 }
			 instanciraneDiplome.Clear();
		 }
		 for (int i = 0; i < score; i++)
		 {
			 GameObject instantiatedPrefab = Instantiate(diplomaPrefab, canvasRectTransform,false);
			 instantiatedPrefab.GetComponent<RectTransform>().anchoredPosition = lokacijaDiplome;
			 instanciraneDiplome.Add(instantiatedPrefab);
			 lokacijaDiplome.x -= 50f;
		 }
		 Debug.Log("Diploma:"+score);
	 }

	 void UpdateHealth()
	 {
		 Vector2 lokacijaSrca = new Vector2(50,-100);
		 if (instanciraniHealth.Count!=0)
		 {
			 if (instanciraniHealth.Count!=health)
			 {
				 PlayerPrefs.SetInt("life",health);
			 }
			 foreach (GameObject healthPrefab in instanciraniHealth)
			 {
				 Destroy(healthPrefab);
			 }
			 instanciraniHealth.Clear();
		 }
		 for (int i = 0; i < health; i++)
		 {
			 GameObject instantiatedPrefab = Instantiate(prefab, canvasRectTransform,false);
			 instantiatedPrefab.GetComponent<RectTransform>().anchoredPosition = lokacijaSrca;
			 instanciraniHealth.Add(instantiatedPrefab);
			 lokacijaSrca.x += 100f;
		 }

		 if (health==0)
		 {
			 gameOver = true;
			 foreach (GameObject objekt in gameOverObjekti)
			 {
				 objekt.SetActive(true);
			 }
			 rb.gravityScale = 0;
			 // this.gameObject.SetActive(false);
		 }
	 }


	 void Awake()
     {
	     if (PlayerPrefs.HasKey("life") )
	     {
		     health = PlayerPrefs.GetInt("life");
	     }
	     if (PlayerPrefs.HasKey("score"))
	     {
		     score = PlayerPrefs.GetInt("score");
	     }
	     gameOver = false;
	     startPosition = transform.position;
	     rb = GetComponent<Rigidbody2D>();
	     naPodu = true;
	     if (audio!=null)
	     {
		     audio.volume = PlayerPrefs.GetFloat("Volume");
	     }
	     noviSpeed = speed;
		 immune = false;
		 gameOverObjekti.Add(gameOverButton);
		 gameOverObjekti.Add(gameOverText);
		 defaultColor = this.gameObject.GetComponent<SpriteRenderer>().color;
     }
	 void OnBecameVisible()
     {
	     UpdateHealth();
	     UdpateScore();
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
	     else if (other.gameObject.CompareTag("Spike"))
	     {
		     health--;
		     UpdateHealth();
		     transform.position = startPosition;
	     }else if (other.gameObject.CompareTag("DamageSpike"))
	     {
		     health--;
		     UpdateHealth();
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

     public IEnumerator MakeImmune()
     {
	     immune = true;
	     this.gameObject.GetComponent<SpriteRenderer>().color=Color.green;
	     yield return new WaitForSeconds(3);
	     this.gameObject.GetComponent<SpriteRenderer>().color=defaultColor;
	     immune = false;
     }

     void OnTriggerEnter2D(Collider2D other)
     {
	     if (other.gameObject.CompareTag("Enemy") && !immune)
	     {
		     StartCoroutine(MakeImmune());
		     health--;
		     UpdateHealth();
	     }
	     else if (other.gameObject.CompareTag("Bonus"))
	     {
		     score++;
		     Destroy(other.gameObject);
		     UdpateScore();
	     }
		 else if (other.gameObject.CompareTag("KnjigaPod"))
	     {
		     health = 0;
		     UpdateHealth();
	     }
	     else if(other.gameObject.CompareTag("DoubleJump"))
	     {
		     rb.AddForce(new Vector2(0,1)*jumpHeight,ForceMode2D.Impulse);
	     }
	     else if (other.gameObject.CompareTag("NextLevel"))
	     {
		     Destroy(other.gameObject);
		     score++;
		     UdpateScore();
		     PlayerPrefs.SetInt("level",PlayerPrefs.GetInt("level")+1);
		     SceneManager.LoadScene("Level"+(PlayerPrefs.GetInt("level")), LoadSceneMode.Single);
	     }
		 else if (other.gameObject.CompareTag("Health"))
	     {
		     Destroy(other.gameObject);
		     health++;
		     UpdateHealth();
	     }
	     else if (other.gameObject.CompareTag("Boss"))
	     {
		     Destroy(other.gameObject);
		     score++;
		     UdpateScore();
		     SceneManager.LoadScene("Victory", LoadSceneMode.Single);
	     }
     }


     void Update()
     {
	     
	     if (!naPodu)
	     {
		     noviSpeed = speed / 3;
	     }
	     else
	     {
		     noviSpeed = speed;
	     }
	     if (Input.GetKey(KeyCode.LeftArrow) && !gameOver)
         {
	         rb.AddForce(new Vector2(-1,0) * noviSpeed);
         }
         else if (Input.GetKey(KeyCode.RightArrow) && !gameOver)
         {
			 rb.AddForce(new Vector2(1,0) * noviSpeed);
         }
	     if(Input.GetKey(KeyCode.Space) && naPodu && !gameOver){
			 rb.AddForce(new Vector2(0,1)*jumpHeight,ForceMode2D.Impulse);
			 naPodu = false;
         }

	     if (Input.GetMouseButtonDown(0))
	     {
		     GameObject clicked = EventSystem.current.currentSelectedGameObject;
		     if (gameOverButton == clicked)
		     {
			     SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		     }
	     }
     }
}

