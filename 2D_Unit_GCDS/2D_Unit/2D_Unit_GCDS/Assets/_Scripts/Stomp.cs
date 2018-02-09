using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Stomp : MonoBehaviour {

	public AudioClip enemyDeathSound;
	public float delay;

	AudioSource audio;
	private Rigidbody2D playerBody;
	public float bounceOnEnemy;

	public GameObject enemyDeathEffect;


	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();
		playerBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
		
	}
	
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {

			StartCoroutine ("DelayDeath");
			Instantiate (enemyDeathEffect, coll.transform.position, coll.transform.rotation);
			playerBody.velocity = new Vector2 (playerBody.velocity.x, bounceOnEnemy);

		}

	}


	IEnumerator DelayDeath () {

		audio.PlayOneShot (enemyDeathSound, 1.0f);
		yield return new WaitForSeconds (delay);
		Destroy (transform.parent.gameObject);


	}
}
