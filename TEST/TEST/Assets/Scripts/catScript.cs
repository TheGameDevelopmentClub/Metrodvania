using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class catScript : EnemyScript {

	
    	private bool alertStage;
    	private bool attackingStage;
    	protected float alertTimer;
    	protected float maxAlertTimer = 1.5f;
	protected bool facingRight;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
		dead = false;
        	health = 1f;

        	attacksReceived = new string[30];

        	movingStage = true;
        	alertStage = false;
        	attackingStage = false;
        	defenceON = false; 

        	alertTimer = 0;
		facingRight = true;
	}
	
	// Update is called once per frame by update()
	override protected void BehaviorUpdate () {


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
		facingRight = true;
            } else {
                transform.localScale = new Vector3(1, 1, 1);
		facingRight = false;
            }
            anim.SetBool("Moving", false);
            alertTimer += Time.deltaTime;

            // Attaking time
            if (alertTimer > maxAlertTimer && Vector2.Distance(transform.position, player.transform.position) < 6f)
            {
		
		alertTimer = 0;
                movingStage = false;
                alertStage = false;
                attackingStage = true;
            }

            //Will chase till player goes away
            if (alertTimer > maxAlertTimer && Vector2.Distance(transform.position, player.transform.position) > 6f)
            {
		alertTimer = 0;
                movingStage = true;
                alertStage = false;
                attackingStage = false;
            }
            
        }
        ///////////////////////
        // Attacking the player  
        //////////////////////
        else if (attackingStage) {

           	anim.SetBool("Moving", true);

            	if (facingRight) {
			// If player jumps over. Wating time to change direction
			if(player.transform.position.x < this.transform.position.x) {
				alertTimer += Time.deltaTime;
				if (alertTimer > maxAlertTimer) {
					alertTimer = 0;
					facingRight = false;								
				}								
			} 

			rigidbody2D.velocity = new Vector2(speed*1.5f, rigidbody2D.velocity.y);
               		transform.localScale = new Vector3(1, 1, 1);

		} else {
			// If player jumps over. Wating time to change direction
			if(player.transform.position.x > this.transform.position.x) {
				alertTimer += Time.deltaTime;
				if (alertTimer > maxAlertTimer) {
					alertTimer = 0;
					facingRight = true;								
				}								
			}

			rigidbody2D.velocity = new Vector2(-speed*1.5f, rigidbody2D.velocity.y);
                	transform.localScale = new Vector3(-1, 1, 1); 
		}

            //Will chase till player goes away
            if (Vector2.Distance(transform.position, player.transform.position) > 6f)
            {
                movingStage = false;
                alertStage = true;
                attackingStage = false;

            }

            // Tries to run away
            if (counting() >= 4)
            {
                movingStage = false;
                alertStage = false;
                attackingStage = false;
            }

        }
	}

    // Dealing damage to the player
    override protected void dealDamage(Collider2D other)
    {

        other.gameObject.GetComponent<playerScript>().takeDamage(1, null);

    }
	
}
