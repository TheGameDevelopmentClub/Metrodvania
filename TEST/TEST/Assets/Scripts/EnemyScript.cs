using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    //Enemy variables
    protected Animator anim;
    protected bool dead;
    protected float health = 3f;

    public GameObject player;    

    //Movement variables
    protected float timer;
    public float maxTimer = 2.5f;
    public float speed = 1f;
    protected float side = 1.0f;
    protected float walkAmount;    

    //Stages of behavior
    protected bool movingStage;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Punch")
        {

            dead = true;
            anim.SetBool("Dead", true);
            anim.SetBool("Moving", false);
            rigidbody2D.velocity = new Vector2(0, 0);

            if (player.transform.position.x - this.transform.position.x <= 0)
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
