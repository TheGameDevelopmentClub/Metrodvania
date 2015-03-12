using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {

	public GameObject player;
	public Image health;
	private float maxHealth;
	
	// Use this for initialization
	void Start () {
		health.fillAmount = 1;
		maxHealth = player.GetComponent<playerScript>().health;
	}
	
	void Update () {
		health.fillAmount = player.GetComponent<playerScript>().health/maxHealth;
	}
}
