using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	
	private Animator anim;
	private bool isDead = false;

	//Jumping variables
	public bool grounded;
	public Transform point1;
	public Transform point2;
	public LayerMask onlyGroundMask; 
	public float jumpForce;
	public float timer;
	private bool canJump;
	public float maxTimer = 0.1f;

	//sprinting variables
	public float initialSpeed = 5f;
	public float speedMultiplier = 2f;
	private float speed; 



	// Use this for initialization
	void Start () {

        grounded = false;
		anim = GetComponent<Animator> ();
	
	}

	// Update is called once per frame
	void Update () {

		if (isDead == true) {

			if (Input.GetKeyDown(KeyCode.R)) 
				Application.LoadLevel(Application.loadedLevel);

			return;
		}

		speed = Input.GetKey (KeyCode.RightShift)? initialSpeed * speedMultiplier : initialSpeed;

		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("velocityY", rigidbody2D.velocity.y);

		if ((Input.GetKey (KeyCode.RightShift)) && grounded && (rigidbody2D.velocity.x != 0)) {
			anim.SetBool ("Run", true);
		} else {
			anim.SetBool ("Run", false);
		}


		if (Input.GetKey (KeyCode.A)) {
			anim.SetBool("Moving", true);
			rigidbody2D.velocity = new Vector2 (-speed, rigidbody2D.velocity.y);
			transform.localScale = new Vector3 (-1, 1, 1);

		} else if (Input.GetKey (KeyCode.D)) {
			anim.SetBool("Moving", true);
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
			transform.localScale = new Vector3 (1, 1, 1);

		} else {
			anim.SetBool("Moving", false);
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);

		}

		//Jumping Code
		grounded = Physics2D.OverlapArea (point1.position, point2.position, onlyGroundMask);
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			canJump = true;
			timer = 0;
			rigidbody2D.AddForce (new Vector2 (0, jumpForce*3));

		} else if ((Input.GetKey (KeyCode.Space)) && canJump && (timer < maxTimer)) {
			timer += Time.deltaTime;
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));

		} else {
			canJump = false;
		
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "deadly") {
			anim.SetBool("Dies", true);
			rigidbody2D.AddForce(new Vector2(0,500));
			isDead = true;
		
		}

	} 
}
