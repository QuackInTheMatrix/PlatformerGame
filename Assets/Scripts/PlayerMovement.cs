using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	 Transform position;
	 Rigidbody2D rb;
	 public float speed=10f;
	 public float jumpHeight=100f;

     void Awake()
     {
         position = GetComponent<Transform> ();
		 rb = GetComponent<Rigidbody2D>();
     }

     void Update()
     {
         // ljevo/desno kretanje
         if (Input.GetKey(KeyCode.LeftArrow))
         {
			 rb.AddForce(new Vector3(-1,0,0) * speed);
         }
         else if (Input.GetKey(KeyCode.RightArrow))
         {
			 rb.AddForce(new Vector3(1,0,0) * speed);
         }
         // skakanje
         if(Input.GetKey(KeyCode.Space)){
			 rb.AddForce(new Vector3(0,1,0)*jumpHeight);
		}
     }
}

