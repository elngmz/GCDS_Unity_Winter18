using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[ RequireComponent (typeof ( AudioSource) ) ]
public class ChangeScene : MonoBehaviour 

{

	public AudioClip uiSound;
	AudioSource audio;


	void Start() {

		audio = GetComponent<AudioSource> ();

	}

	public void ChangeToScene (int SceneToChangeTo) {

		SceneManager.LoadScene (SceneToChangeTo);

		StartCoroutine ("Delay"); //starts coroutine

		if (SceneToChangeTo == 10) {

			Application.Quit (); //quits build, does not work in editor

		}

	}

	IEnumerator Delay () {

		audio.PlayOneShot (uiSound, 1.0f);
		yield return new WaitForSeconds (1);

	}

}
