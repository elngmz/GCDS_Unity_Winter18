using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour 

{

	private PlayerController player; //gives access to the script


	void Start () 

	{

		player = gameObject.GetComponentInParent<PlayerController> (); //player controller script is in parent... get component from it

	}

	void OnTriggerEnter2D ( Collider2D col ) 

	{
		
		player.m_isGrounded = true;

	}

	void OnTriggerStay2D( Collider2D col ) 

	{
		player.m_isGrounded = true;

	}

	void OnTriggerExit2D ( Collider2D col ) 

	{
		player.m_isGrounded = false;

	}
}
