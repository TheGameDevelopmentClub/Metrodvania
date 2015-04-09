using UnityEngine;
using System.Collections;

public class switchScript : MonoBehaviour {

	Animator anim;
	//public movingGrass move;
	public GameObject obj;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
		pos = obj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D() {
		obj.transform.position = pos;
	}

	void OnTriggerStay2D() {

		anim.SetBool("Down", true);
		//move.Toggle(true); 
	}

	void OnTriggerExit2D() {
		
		anim.SetBool("Down", false);
	}
}
