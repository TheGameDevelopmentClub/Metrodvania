using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{
	Animator anim;
	bool dead;
	public bool punch;
    
	//Jump variables
	public bool grounded;
	public Transform point1;
	public Transform point2;
	public LayerMask onlyGroundLayer;
	public float jumpForce = 75f;
	public float timer;
	bool canJump;
	public float maxTimer = 0.2f;

    //Combat Variables
    public float health = 10f;
    private float damageTimer = 2f;
    private float damageWait = 2f;
    public GameObject punchArea;

	//Speed Variables
	float speed = 2f;
	public float initialSpeed = 2f;
	public float runMultiplier = 1.5f;
    bool canRun;
    public GameObject particles;

    //Dash variables 
    public bool canMove;
    private float dashMaxTime = 0.15f;
    private float dashTimer;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent <Animator> ();
		dead = false;
        health = 10f;
        punchArea.gameObject.SetActive(false);
        canMove = true;
        dashTimer = 1f;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//When dead
		if(dead){

			if(Input.GetKey(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}

			rigidbody2D.velocity = new Vector2(0,0);
			//collider.enabled = false;
			return;
		}

        if (damageTimer < damageWait)
        {
            damageTimer += Time.deltaTime;
        }
        
        //damage blinking
        if (damageTimer <= (damageWait/4)) this.renderer.enabled = false;
        else if (damageTimer > (damageWait / 4) && damageTimer <= (damageWait / 2)) this.renderer.enabled = true;
        else if (damageTimer > (damageWait/ 2) && damageTimer < (damageWait * 3 / 4)) this.renderer.enabled = false;
        else if (damageTimer >= (damageWait / 4)) this.renderer.enabled = true;        


		//Sprint
        speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("LB")) ? initialSpeed * runMultiplier : initialSpeed;
        if (grounded && rigidbody2D.velocity.x != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("LB")) && canRun)
        {

            particles.SetActive(true);
			anim.SetBool("Run", true);
		} else {
            particles.SetActive(false);
			anim.SetBool("Run", false);
		}


		// Moving to the Sides                 //This blocks the movements
		if ((Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("D-Pad X") == 1)&& canMove) {
			anim.SetBool("Moving", true);
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
			transform.localScale = new Vector3(1, 1 ,1);
            particles.transform.rotation = Quaternion.Euler(new Vector3(302.5309f, 270f, 90f));

        }                               
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("D-Pad X") == -1) && canMove)
        {
			anim.SetBool("Moving", true);
			rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
			transform.localScale = new Vector3(-1, 1 ,1);
            particles.transform.rotation = Quaternion.Euler(new Vector3(302.5309f, 270f, -90f));

		} else {
			anim.SetBool("Moving", false);
			rigidbody2D.velocity = new Vector2(0 , rigidbody2D.velocity.y);

		}

		//Jumping
		grounded = Physics2D.OverlapArea(point1.position, point2.position, onlyGroundLayer);
		anim.SetBool("Grounded", grounded);
		anim.SetFloat("velocityY", rigidbody2D.velocity.y);

		if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A"))) {

			timer = 0;
			canJump = true;
			rigidbody2D.AddForce(new Vector2(0, 3*jumpForce));
            anim.SetBool("Push", false);
		
		} else if ((Input.GetKey(KeyCode.Space) || Input.GetButton("A")) && canJump && timer<maxTimer) {

			timer += Time.deltaTime;
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
            anim.SetBool("Push", false);
		
		} else {

			canJump = false;
		}

     

		//Dead by jump of map
		if (transform.position.y < -10) {
			//Application.LoadLevel(Application.loadedLevel);
		}

		//Punch animation
		if (grounded && (Input.GetKey(KeyCode.Z) || Input.GetButtonDown("B"))) {

			anim.SetBool("Punch", true);
			punch = true;
            punchArea.gameObject.SetActive(true);
		} 

		if (!punch) {
			anim.SetBool("Punch", false);
            punchArea.gameObject.SetActive(false);
        }


        //Dash movement when press the X bottom. Can only be done every 1.5 secs
        if ( (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("LB")) && dashTimer > 1f) {

            
            canMove = false;
            dashTimer = 0f;
        }
        dashTimer += Time.deltaTime;

        if (dashTimer > dashMaxTime) {

            anim.SetBool("Dash", false);
            canMove = true;
        }
        else {
            if (grounded) {anim.SetBool("Dash", true); }
            
            if (transform.localScale.x < 0)
            {
                this.rigidbody2D.AddForce(new Vector2(8, 0), ForceMode2D.Impulse);
            }
            else
            {
                this.rigidbody2D.AddForce(new Vector2(-8, 0), ForceMode2D.Impulse);
            }
        }
	
	}

    ///////////////////////////
    ///     Player Damage
    ///////////////////////////

	void OnTriggerEnter2D(Collider2D other) {

        //if hit enemy
        if (other.tag == "deadly" && punch && ((this.transform.localScale.x>0 && other.gameObject.transform.localScale.x<0)
            || (this.transform.localScale.x < 0 && other.gameObject.transform.localScale.x > 0)))
        {
            other.gameObject.GetComponent<foeScript>().takeDamage(1, null, null);
		}

        //if Hit by projectile
        if (other.tag == "projectile" && !dead && damageTimer >= damageWait) {
            takeDamage(2, null);
            anim.SetBool("Punch", false);
        }

		//If hit health potion
        if (other.tag == "healthPotion") {
			Destroy(other.gameObject);
            health += 5f;
            if (health > 10f) {
                health = 10f;
            }
        }
	}
    
    public bool takeDamage(int damageAmount, string effect) 
    {
        if (damageTimer >= damageWait && !dead)
        {
            damageTimer = 0f;
            health -= damageAmount;
            if (health <= 0f)
            {
                anim.SetBool("Run", false);
                anim.SetBool("Moving", false);
                anim.SetBool("Death", true);
                anim.SetBool("Punch", false);
                anim.SetBool("Grounded", true);
                dead = true;
                rigidbody2D.AddForce(new Vector2(0, 100));
            }
            else
            {
                //anim.SetBool("DamageTaken", true);

            }

            return true;
        }
        else
        {
            return false;
        }
    }


    ///////////////////////////
    ///     Mooving box
    ///////////////////////////

    void OnTriggerStay2D(Collider2D other) {

        if ((other.tag == "box") && rigidbody2D.velocity.y == 0 && !dead)
        {
            canRun = false;
            anim.SetBool("Push", true);           
        }

    }

    void OnTriggerExit2D(Collider2D other) {

        if ((other.tag == "box") && rigidbody2D.velocity.y == 0 && !dead)
        {
            canRun = true;
            anim.SetBool("Push", false);        
        }

    }

    
}
