using UnityEngine;
using System.Collections;

public class foeScript : MonoBehaviour {

	Animator anim;
	public playerScript player;
	bool dead;

	float timer;
	public float maxTimer = 2.5f;
	public float speed = 1f;
	private float side = 1.0f;
	private float walkAmount;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
		dead = false;

	}
	
	// Update is called once per frame
	void Update () {

		//When dead
		if (dead) {

			//rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			return;
		}

		anim.SetBool("Moving", true);
		walkAmount = side * speed;
		timer += Time.deltaTime;

		if (timer > maxTimer) {
			side = side * -1.0f;
			timer = 0;

		} 

		rigidbody2D.velocity = new Vector2 (walkAmount, rigidbody2D.velocity.y);
		transform.localScale = new Vector3 (side, 1, 1);
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "player" && player.punch) {

            dead = true;
			anim.SetBool("Dead", true);
			anim.SetBool("Moving", false);
            rigidbody2D.velocity = new Vector2(0, 0);
			rigidbody2D.AddForce(new Vector2(100, 50));
			Destroy(this.gameObject, 1f);
			gameObject.tag = "neutralized";
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		
		if (other.tag == "player" && player.punch) {
			
			dead = true;
			anim.SetBool("Dead", true);
			anim.SetBool("Moving", false);
			rigidbody2D.velocity = new Vector2(0, 0);
			rigidbody2D.AddForce(new Vector2(100, 50));
			Destroy(this.gameObject, 1f);
			gameObject.tag = "neutralized";
		}
	}
	
}
