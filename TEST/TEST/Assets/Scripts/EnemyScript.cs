using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {

    //Enemy variables
    protected Animator anim;
    protected bool dead;
    protected float health;
    public GameObject player;

    // "Learning" AI
    protected List<string> attacksReceived;
    protected string lastAttack;

    //Movement variables
    protected float timer;
    protected float maxTimer = 2.5f;
    protected float speed = 1f;
    protected float side = 1.0f;
    protected float walkAmount;    

    //Stages of behavior
    protected bool movingStage;
    protected bool defenceON; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Taking damage from player
    public bool takeDamage (int damage, string type, string effect)
    {
        if (!defenceON)
        {
            health -= damage;
            attacksReceived.Add(type);
            lastAttack = type;

            if (player.transform.position.x - this.transform.position.x <= 0)
            {
                rigidbody2D.AddForce(new Vector2(100, 50));
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(-100, 50));
            }

            if (health <= 0)
            {
                dead = true;
                anim.SetBool("Dead", true);
                anim.SetBool("Moving", false);
                rigidbody2D.velocity = new Vector2(0, 0);
                Destroy(this.gameObject, 1f);
                gameObject.tag = "neutralized";
            }
            return true;
        }
        else 
        {
            return false;
        }
        
    }

    //Attacking the player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            if (this is foeScript)
            {
                this.dealDamage(other);
            }
        }
    }

    protected virtual void dealDamage(Collider2D other) { 
    
    }

    // Count the number of occurrences of the las attack
    protected int counting()
    {
        int count = 0;
        foreach (string value in attacksReceived)
        {
            if (value.Equals(lastAttack))
            {
                count++;
            }
        }


        return count;
    }
}
