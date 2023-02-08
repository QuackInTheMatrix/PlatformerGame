using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayer : MonoBehaviour
{
     Rigidbody2D rb;
	 public float speed=10f;
	 public float jumpHeight=10f;
	 private bool naPodu;

	 void Awake()
     {
	     rb = GetComponent<Rigidbody2D>();
	     naPodu = true;
     }
     void OnCollisionEnter2D(Collision2D collision)
     {
	     if (collision.gameObject.CompareTag("Pod"))
	     {
		     naPodu = true;
	     }
     }
     
     void Update()
     {
         // ljevo/desno kretanje
         if (Input.GetKey(KeyCode.LeftArrow))
         {
	         rb.AddForce(new Vector2(-1,0) * speed);
         }
         else if (Input.GetKey(KeyCode.RightArrow))
         {
			 rb.AddForce(new Vector2(1,0) * speed);
         }
         // skakanje
         if(Input.GetKey(KeyCode.Space) && naPodu){
			 rb.AddForce(new Vector2(0,1)*jumpHeight,ForceMode2D.Impulse);
			 naPodu = false;
         }
     }
}
