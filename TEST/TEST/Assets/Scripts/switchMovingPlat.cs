using UnityEngine;
using System.Collections;

public class switchMovingPlat : MonoBehaviour {

	Animator anim;
	public movingPlat obj;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerStay2D() {

		anim.SetBool("Down", true);
        obj.Toggle(); 
	}

	void OnTriggerExit2D() {
		
		anim.SetBool("Down", false);
	}
}
