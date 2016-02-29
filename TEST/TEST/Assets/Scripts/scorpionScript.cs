using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scorpionScript : EnemyScript {

	
    	private bool alertStage;
    	private bool attackingStage;
    	protected float alertTimer;
    	protected float maxAlertTimer = 1.5f;
	protected GameObject tail;
	protected bool up;

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
		up = true;
	}
	
	// Update is called once per frame by update()
	override protected void BehaviorUpdate () {

		GetComponent<Rigidbody2D>().isKinematic = true;		

		//When dead
		if (dead) {

			//rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			return;
		}
		
		////////////////////////
		// Moving stage
		///////////////////////

		if (movingStage) {
			
			anim.SetBool("Moving", true);
            		timer += Time.deltaTime;

            		if (timer > maxTimer)
            		{
                		up = !up;
				timer = 0;

            		}

			if (up) {
				
				transform.Translate(Vector3.up*Time.deltaTime);
			}
			else {
				
				transform.Translate(Vector3.down*Time.deltaTime);
			}
			
		}

		//Need to add the sting
        
	}

    // Dealing damage to the player
    override protected void dealDamage(Collider2D other)
    {

        other.gameObject.GetComponent<playerScript>().takeDamage(1, null);

    }
	
}
