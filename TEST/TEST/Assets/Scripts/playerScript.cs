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

	//Speed Variables
	float speed = 2f;
	public float initialSpeed = 2f;
	public float runMultiplier = 1.5f;
    bool canRun;
    public GameObject particles;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent <Animator> ();
		dead = false;
        health = 10f;
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
        if (damageTimer > (damageWait / 4) && damageTimer <= (damageWait / 2)) this.renderer.enabled = true;
        if (damageTimer > (damageWait/ 2) && damageTimer < (damageWait * 3 / 4)) this.renderer.enabled = false;
        if (damageTimer >= (damageWait / 4)) this.renderer.enabled = true;           


        


        //else if (anim.GetBool("DamageTaken"))
        //{
            //anim.SetBool("DamageTaken", false);
        //}

		//Sprint
		speed = Input.GetKey(KeyCode.LeftShift)? initialSpeed*runMultiplier : initialSpeed;
		if(grounded && rigidbody2D.velocity.x != 0 && Input.GetKey(KeyCode.LeftShift) && canRun) {

            particles.SetActive(true);
			anim.SetBool("Run", true);
		} else {
            particles.SetActive(false);
			anim.SetBool("Run", false);
		}


		// Moving to the Sides
		if (Input.GetKey(KeyCode.RightArrow)) {
			anim.SetBool("Moving", true);
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
			transform.localScale = new Vector3(1, 1 ,1);
            particles.transform.rotation = Quaternion.Euler(new Vector3(302.5309f, 270f, 90f));
			
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
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

		if (grounded && Input.GetKeyDown(KeyCode.Space)) {

			timer = 0;
			canJump = true;
			rigidbody2D.AddForce(new Vector2(0, 3*jumpForce));
            anim.SetBool("Push", false);
		
		} else if (Input.GetKey(KeyCode.Space) && canJump && timer<maxTimer) {

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
		if (Input.GetKeyDown(KeyCode.Z)) {

			anim.SetBool("Punch", true);
			punch = true;

		} 

		if (!punch) {
			anim.SetBool("Punch", false);
		}
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		//If hit death wall
		if (other.tag == "deathwall" && !dead) 
		{
			health = 0;
			OnHit();
		}

        //if hit by enemy
        if (other.tag == "deadly" && !dead && !punch && damageTimer >= damageWait)
        {
            health -= 2;
            OnHit();
		}

        //if Hit by projectile
        if (other.tag == "projectile" && !dead && damageTimer >= damageWait) {
            health -= 2;
            OnHit();
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

    void OnTriggerStay2D(Collider2D other) {

        if ((other.tag == "box") && rigidbody2D.velocity.y == 0 && !dead)
        {
            canRun = false;
            anim.SetBool("Push", true);           
        }

        //if hit by enemy
        if (other.tag == "deadly" && !dead && !punch && damageTimer >= damageWait)
        {
            health -= 2;
            OnHit();
        }

        //if Hit by projectile
        if (other.tag == "projectile" && !dead && damageTimer >= damageWait)
        {
            health--;
            OnHit();
            anim.SetBool("Punch", false);
        }

        //If player is on a moving platform
        if (other.tag == "movePlat") {
            this.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, this.rigidbody2D.velocity.y);
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if ((other.tag == "box") && rigidbody2D.velocity.y == 0 && !dead)
        {
            canRun = true;
            anim.SetBool("Push", false);        
        }
    }

    void OnHit()
    {
        damageTimer = 0f;
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
    }
}
