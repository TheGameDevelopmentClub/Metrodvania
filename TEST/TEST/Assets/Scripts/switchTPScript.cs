using UnityEngine;
using System.Collections;

public class switchTPScript : MonoBehaviour {

	Animator anim;
	//public movingGrass move;
	public GameObject obj;
	public GameObject destination;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D() {
		obj.transform.position = destination.transform.position;
	}

	void OnTriggerStay2D() {

		anim.SetBool("Down", true);
		//move.Toggle(true); 
	}

	void OnTriggerExit2D() {
		
		anim.SetBool("Down", false);
	}
}
