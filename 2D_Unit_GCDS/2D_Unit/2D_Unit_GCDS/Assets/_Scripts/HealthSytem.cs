using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSytem : MonoBehaviour {

	public Sprite fullHearts;
	public Sprite twoHearts;
	public Sprite oneHeart;
	public Sprite emptyHearts;

	public Image heartsUI;

	private PlayerController player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (player.m_curHealth == 3) {

			heartsUI.sprite = fullHearts;

		}

		if (player.m_curHealth == 2) {

			heartsUI.sprite = twoHearts;

		}

		if (player.m_curHealth == 1) {

			heartsUI.sprite = oneHeart;

		}

		if (player.m_curHealth == 0) {

			heartsUI.sprite = emptyHearts;

		}

	}

}
