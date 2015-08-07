using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class foeScript : EnemyScript {

	
    private bool alertStage;
    private bool attackingStage;
    private bool evasiveStage;
    protected float alertTimer;
    protected float maxAlertTimer = 1.5f;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
		dead = false;
        health = 5f;
        damageTimer = 0f;
        damageWait = 0.7f;

        attacksReceived = new string[30];

        movingStage = true;
        alertStage = false;
        attackingStage = false;
        evasiveStage = false;
        defenceON = false; 

        alertTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

        damageTimer += Time.deltaTime;

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
                evasiveStage = false;
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
            if (alertTimer > maxAlertTimer && Vector2.Distance(transform.position, player.transform.position) < 6f)
            {

                movingStage = false;
                alertStage = false;
                attackingStage = true;
                evasiveStage = false;
            }

            //Will chase till player goes away
            if (alertTimer > maxAlertTimer && Vector2.Distance(transform.position, player.transform.position) > 6f)
            {
                movingStage = true;
                alertStage = false;
                attackingStage = false;
                evasiveStage = false;
            }
            
        }
        ///////////////////////
        // Attacking the player  
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
                movingStage = false;
                alertStage = true;
                attackingStage = false;
                evasiveStage = false;
            }

            // Tries to run away
            if (counting() >= 4)
            {
                movingStage = false;
                alertStage = false;
                attackingStage = false;
                evasiveStage = true;
            }

        }

        ///////////////////////
        /// Evasive manouver
        ///////////////////////
        else if (evasiveStage)
        {
            anim.SetBool("Moving", true);

            if (player.transform.position.x > this.transform.position.x)
            {

                rigidbody2D.velocity = new Vector2(-speed * 1.5f, rigidbody2D.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {

                rigidbody2D.velocity = new Vector2(speed * 1.5f, rigidbody2D.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            //Will chase till player goes away
            if (Vector2.Distance(transform.position, player.transform.position) > 6f)
            {
                movingStage = true;
                alertStage = false;
                attackingStage = false;
                evasiveStage = false;
            }

        }
	}

    // Dealing damage to the player
    override protected void dealDamage(Collider2D other)
    {

        other.gameObject.GetComponent<playerScript>().takeDamage(2, null);

    }
	
}
