using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> (); 
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {


		if (other.gameObject.tag == "Player") {

			player.m_hurt = true;
			player.Damage (1);


		} else {

			player.m_hurt = false;

		}
		
	}


	void OnTriggerExit2D ( Collider2D other) {

		if (other.gameObject.tag == "Player") {

			player.m_hurt = false;

		}


	}
}
