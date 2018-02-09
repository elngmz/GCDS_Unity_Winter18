using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {

			player.m_onLadder = true;


		}


	}


	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player") {

			player.m_onLadder = false;


		}

	}
}
