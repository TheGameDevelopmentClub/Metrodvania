using UnityEngine;
using System.Collections;

public class foeScript : MonoBehaviour {

	Animator anim;
	public playerScript player;
	bool dead;
    public GameObject pl;

	float timer;
	public float maxTimer = 2.5f;
	public float speed = 1f;
	private float side = 1.0f;
	private float walkAmount;

    private float alertTimer;
    private float maxAlertTimer = 1.5f;


    //Stages of behavior
    public bool movingStage;
    public bool alertStage;
    public bool attackingStage;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
		dead = false;

        movingStage = true;
        alertStage = false;
        attackingStage = false;

        alertTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//When dead
		if (dead) {

			//rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			return;
		}
        ///////////////////////////////
        // Normal moving. No player near
        //////////////////////////////
        if (movingStage)
        {

            anim.SetBool("Moving", true);
            walkAmount = side * speed;
            timer += Time.deltaTime;

            if (timer > maxTimer)
            {
                side = side * -1.0f;
                timer = 0;

            }

            rigidbody2D.velocity = new Vector2(walkAmount, rigidbody2D.velocity.y);
            transform.localScale = new Vector3(side, 1, 1);

            // If player close enough change stage
            if (Vector2.Distance(transform.position, player.transform.position) <= 6f) {
                movingStage = false;
                alertStage = true;
                attackingStage = false;
            } 
        }
        //////////////////////////////////////////
        // A moment of realizing the enemy is near
        //////////////////////////////////////////
        else if (alertStage) {

            if (player.transform.position.x < this.transform.position.x) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else {
                transform.localScale = new Vector3(1, 1, 1);
            }
            anim.SetBool("Moving", false);
            alertTimer += Time.deltaTime;

            // Attaking time
            if (alertTimer > maxAlertTimer) {

                movingStage = false;
                alertStage = false;
                attackingStage = true;
            }
            
        }
        ///////////////////////
        // Attacking the enemy  
        //////////////////////
        else if (attackingStage) {

            anim.SetBool("Moving", true);

            if (player.transform.position.x < this.transform.position.x) {

                rigidbody2D.velocity = new Vector2(-speed*1.5f, rigidbody2D.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {

                rigidbody2D.velocity = new Vector2(speed*1.5f, rigidbody2D.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            //Will chase till player goes away
            if (Vector2.Distance(transform.position, player.transform.position) > 6f)
            {
                movingStage = true;
                alertStage = false;
                attackingStage = false;
            } 

        }
	}

	void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Punch")
        {

            dead = true;
            anim.SetBool("Dead", true);
            anim.SetBool("Moving", false);
            rigidbody2D.velocity = new Vector2(0, 0);

            if (pl.transform.position.x - this.transform.position.x <= 0)
            {
                rigidbody2D.AddForce(new Vector2(100, 50));
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(-100, 50));
            }
            Destroy(this.gameObject, 1f);
            gameObject.tag = "neutralized";
        }
	}

	void OnTriggerStay2D(Collider2D other) {
		
		if (other.tag == "Punch") {
			
			dead = true;
			anim.SetBool("Dead", true);
			anim.SetBool("Moving", false);
			rigidbody2D.velocity = new Vector2(0, 0);

            if (pl.transform.position.x - this.transform.position.x <= 0)
            {
                rigidbody2D.AddForce(new Vector2(100, 50));
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(-100, 50));
            }
			Destroy(this.gameObject, 1f);
			gameObject.tag = "neutralized";
		}
	}
	
}
