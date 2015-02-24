using UnityEngine;
using System.Collections;

public class foeScript : MonoBehaviour {

	public float speed = 1f;
	public float wallLeft = 0.0f;
	public float wallRight = 5.0f;
	public float health = 3;

	private float side = 1.0f;
	private float walkAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		walkAmount = side * speed;

		if (side > 0.0f && transform.position.x > wallRight) {
			side = -1.0f;

		} else if (side <= 0.0f && transform.position.x < wallLeft) {
			side = 1.0f;

		}

		rigidbody2D.velocity = new Vector2 (walkAmount, rigidbody2D.velocity.y);
		transform.localScale = new Vector3 (side, 1, 1);
		//transform.Translate(walkAmount);
	}

	void OnHit(){
		health--;
	}
}
