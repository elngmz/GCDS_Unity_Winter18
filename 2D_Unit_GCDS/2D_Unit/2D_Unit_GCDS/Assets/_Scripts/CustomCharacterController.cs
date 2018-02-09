using System.Collections;//add to use IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement; // add to load scenes

[ RequireComponent ( typeof ( AudioSource ) ) ]
public class CustomCharacterController : MonoBehaviour 

{

	public float m_maxSpeed;
	public float m_speed; //how fast character walks
	public float m_jumpForce; //how much force we add when character jumps

	public bool m_grounded; //checks if the player is on a platform
	public bool m_deathCheck; //checks if player is dead
	public bool m_hurt; //checks if player is damaged

	private Rigidbody2D m_rigiBody; //gives access to the RigidBOdy2D of the controller gameobject

	private Animator m_anim; //gives access to the Animator of the controller gameobject

	public AudioClip m_jumping; // jumping audio file

	AudioSource audio; //audio source setup



	void Start () 

	{

		m_rigiBody = gameObject.GetComponent<Rigidbody2D> (); //getting RigidBOdy2D info from gameobject

		m_anim = gameObject.GetComponent<Animator>(); //getting Animator info from gameobject

		audio = GetComponent<AudioSource> (); // access audio source component


	}
		

	void Update () 

	{

		//Debug.Log ("max speed " + m_maxSpeed + ", speed:" + m_speed);
		m_anim.SetBool ( "IsGrounded", m_grounded ); //setting our IsGrounded animator parameter
		m_anim.SetFloat ( "Speed", Mathf.Abs (m_rigiBody.velocity.x)); //setting our Speed animator parameter || Mathf.Abs make it so that when we move left in negative, it's always a positive number



		//adds integrated input to move right and left arrow keys
		float h = Input.GetAxis( "Horizontal" ); 

		//flipping character's sprite to face correct direction on X axis
		if (Input.GetAxis ( "Horizontal" ) < -.001f) 
		
		{

			transform.localScale = new Vector3 ( -2.2f, 2.2f, 2.2f );

		}

		//Moving the character
		if (m_grounded) 
		
		{

			m_rigiBody.AddForce (( Vector2.right * m_speed ) * h); //moves the player when you move left and right

		}

		if (Input.GetAxis ( "Horizontal" ) > .001f) 
		
		{

			transform.localScale = new Vector3 ( 2.2f, 2.2f, 2.2f );



		}

		if ( Input.GetKeyDown ( KeyCode.Space ) && m_grounded ) 
		
		{

			m_rigiBody.AddForce ( Vector2.up * m_jumpForce );

			audio.PlayOneShot ( m_jumping, 1.0f ); //play sfx

		}

		if ( !m_grounded ) 
		
		{

			m_speed = 300f;

		} else 
		
		{

			m_speed = 400f;
		}


	}
		

	void FixedUpdate () 

	{

		Vector3 easeVelocity = m_rigiBody.velocity;
		easeVelocity.y = m_rigiBody.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		if (m_grounded) 
		
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

}