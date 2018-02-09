using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour {

	#region Vairable Declarations

	//movement and physics
	public float m_speed;
	public float m_maxSpeed;
	public float m_jumpForce;

	public bool m_isGrounded;

	private Rigidbody2D m_rigiBody;
	private Animator m_anim;

	//sound and level
	public AudioClip m_jumpSfx;
	public AudioClip m_coinCollect;
	public AudioClip m_damageSfx;

	AudioSource m_audio;
	public GameObject m_respawn;
	//private GameMaster gm;

	//health stats
	public int m_curHealth; //current health integer
	public int m_maxHealth = 3; //full health is 3
	public bool m_deathCheck; //checks if player is dead
	public bool m_hurt; //checks if player is damaged


	//projectile
	public Transform bulletPoint;
	public GameObject bullet;

	//game Over overlay
	//public GameObject gameOverScreen;

	//ladder
	public bool m_onLadder;
	public float m_climbSpeed;
	private float m_climbVelocity;
	private float m_gravityStorage;

	#endregion 

	void Start () { 

		m_rigiBody = gameObject.GetComponent<Rigidbody2D> (); //get give us access to Rigifbody2D component
		m_anim = gameObject.GetComponent<Animator>(); //get access to Animator component

		m_audio = GetComponent<AudioSource> (); // get access to Audio component

		//gm = GameObject.FindGameObjectWithTag ("Game Master").GetComponent<GameMaster> (); // get access to Game Master script

		//gameOverScreen.SetActive (false);

		m_gravityStorage = m_rigiBody.gravityScale;

	}
	
	#region Movement and Input Conditionals

	void Update () {

		m_anim.SetBool ("IsGrounded", m_isGrounded); //setting grounded value in animation
		m_anim.SetFloat ("Speed", Mathf.Abs(m_rigiBody.velocity.x)); // setting speed value in animation; Mathf.Abs allows us to use the absolute value of the variable
		m_anim.SetBool("IsAlive", m_deathCheck); //setting ISAlive animation parameter
		m_anim.SetBool("IsDamaged", m_hurt); //setting IsDamaged animation parameter


		float h = Input.GetAxis ("Horizontal");


		if (Input.GetAxis ("Horizontal") < -.001f ) {

			transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);

		}

		if (m_isGrounded) {
			 
			m_rigiBody.AddForce ((Vector2.right * m_speed) * h); //moved player left and right


		}

		if (Input.GetAxis ("Horizontal") > .001f ) {

			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

		}
			

		if (!m_isGrounded) {

			m_speed = 150f;

		}

		else {

			m_speed = 200f;

		}

		if (m_curHealth > m_maxHealth) {

			m_curHealth = m_maxHealth; // always sets health to max (3) at start of game

		}

		if (m_curHealth <= 0) {

			StartCoroutine ("DelayedRestart");

		}

		if (Input.GetKeyDown (KeyCode.Z)) {


			Instantiate (bullet, bulletPoint.position, bulletPoint.rotation);

		}
			
		if (Input.GetKeyDown (KeyCode.R)) {

			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		}
			

		if (!m_deathCheck) {

			Time.timeScale = 1;

		}

		if (m_onLadder) {

			m_rigiBody.gravityScale = 0f;
			m_climbVelocity = m_climbSpeed;
			m_rigiBody.velocity = new Vector2 (m_rigiBody.velocity.x, m_climbVelocity);


		}

		if (!m_onLadder) {

			m_rigiBody.gravityScale = m_gravityStorage;

		}

		if (Input.GetKeyDown (KeyCode.Space) && m_isGrounded) {


			m_rigiBody.AddForce (Vector2.up * m_jumpForce);
			m_audio.PlayOneShot (m_jumpSfx, 1.0f) ; 
		}
	 
			


	}

	#endregion

	#region Collision Functionality

	void OnTriggerEnter2D(Collider2D col) {

		if (col.CompareTag ("Coin")) {
			
			Destroy (col.gameObject); //Destroyedcoin      
			//gm.points += 1;
			m_audio.PlayOneShot (m_coinCollect, 1.0f); // Play the coin sound
		}


		if (col.CompareTag ("Kill Zone")) {

			transform.position = m_respawn.transform.position;

		}
	

		if (col.CompareTag ("Level 2 Trigger")) {

			SceneManager.LoadScene ("Level 2");

			Debug.Log ("SCENE CHANGED");
				
		}


	}

	#endregion

	void FixedUpdate () 

	{

		Vector3 easeVelocity = m_rigiBody.velocity;
		easeVelocity.y = m_rigiBody.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.0f;

		if (m_isGrounded) 

		{

			m_rigiBody.velocity = easeVelocity;

		}

		//Limiting the speed of the character*
		if ( m_rigiBody.velocity.x > m_maxSpeed ) 

		{

			m_rigiBody.velocity = new Vector2 ( m_maxSpeed, 0f );

		}

		if ( m_rigiBody.velocity.x < -m_maxSpeed ) 

		{

			m_rigiBody.velocity = new Vector2 ( -m_maxSpeed, 0f );

		}
	
	}

	#region Death and Damage Functionality

	//void Death () {
	
		//m_deathCheck = true;
		//gameOverScreen.SetActive (true); //turning on game over screen

		//if (m_deathCheck){

			//Debug.Log ("Player is Dead");
			//Time.timeScale = 0; //pause or freeze the game


		//}

	//}

	IEnumerator DelayedRestart () {

		yield return new WaitForSeconds (1); //delay by time
		//Death();


	}

	public void Damage (int dmg) {

		m_audio.PlayOneShot (m_damageSfx, 1.0f); //play damage sound effect
		m_curHealth -= dmg; //take negative damage integer


	}

	#endregion


}
