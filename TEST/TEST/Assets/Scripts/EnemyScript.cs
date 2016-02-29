using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {

    //Enemy variables
    protected Animator anim;
    protected bool dead;
    protected float health;
    public GameObject player;
    protected float damageTimer = 0f;
    protected float damageWait = 0.7f;

    // "Learning" AI
    protected string[] attacksReceived = new string[30];
    protected string lastAttack;
    protected int arrayPos = 0;

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
		damageTimer += Time.deltaTime;
        BehaviorUpdate();
	}
	
	// Behavior Function
	protected virtual void BehaviorUpdate() {}

	

    // Taking damage from player
    public bool takeDamage (int damage, string type, string effect)
    {
        if (!defenceON && damageTimer >= damageWait)
        {
            damageTimer = 0f;
            health -= damage;
            add(type);
            lastAttack = type;

            if (player.transform.position.x - this.transform.position.x <= 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 50));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 50));
            }

            if (health <= 0)
            {
                dead = true;
                anim.SetBool("Dead", true);
                anim.SetBool("Moving", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
    // "Abstract" of dealDamage
    protected virtual void dealDamage(Collider2D other) { }


    // Add attckat to the array
    protected void add(string att)
    {
        attacksReceived[arrayPos] = att;
        arrayPos++;
    }

    // Count the number of occurrences of the las attack
    protected int counting()
    {
        int count = 0;
        int x = 0;
        while (x < arrayPos)
        {
            if (attacksReceived[x].Equals(lastAttack))
            {
                count++;
            }
            x++;
        }

        return count;
    }
}
