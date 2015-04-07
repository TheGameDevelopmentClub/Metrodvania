using UnityEngine;
using System.Collections;

public class switchScript : MonoBehaviour {

	Animator anim;
	public movingGrass move;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D() {

		anim.SetBool("Down", true);

		move.Toggle(true); 
	}

	void OnTriggerExit2D() {
		
		anim.SetBool("Down", false);
	}
}
