﻿using UnityEngine;
using System.Collections;

//DONT USE THIS, IT DESTROYS THE PICKUP BEFORE RESTORING HEALTH
public class collectibles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "player") {

			Destroy(gameObject);
			//Add player life
		}
	}
}
