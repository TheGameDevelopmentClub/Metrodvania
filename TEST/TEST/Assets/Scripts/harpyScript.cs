using UnityEngine;
using System.Collections;

public class harpyScript : MonoBehaviour {

    public GameObject player;
    public bool playerNear = false;
    public float initialPositionY = 0.18f;
    public float upDownSpeed = 1.5f;
    public float sidesSpeed = 1f;
    private float direction = 1f;
    private float time = 0;
    private float side = -1f;
    public float attSpeed = 3f;

    public bool fix = false;
    private Vector2 playerPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Check position of player
        if (Vector2.Distance(transform.position, player.transform.position) < 7f)
        {

            playerNear = true;

        }
        else {

            playerNear = false;
        }

        //Iddle moving
        if (!playerNear && !fix)
        {

            //Up - Down
            if (transform.position.y > (initialPositionY + 0.2f) || transform.position.y < (initialPositionY - 0.2f))
            {
                direction = direction * -1;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x, (direction * upDownSpeed));

            //Sides movement
            time += Time.deltaTime;

            if (time > 2.5f)
            {
                side = side * -1.0f;
                time = 0;

            }
            GetComponent<Rigidbody2D>().velocity = new Vector2((side * sidesSpeed), GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(-side, 1, 1);

        }
        if (playerNear || fix) {

            if (!fix)
            {

                if (Vector2.Distance(transform.position, player.transform.position) < 1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attSpeed * Time.deltaTime);
                    playerPos = player.transform.position;
                    fix = true;
                }
                else
                {

                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attSpeed * Time.deltaTime);
                }
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, playerPos, attSpeed * Time.deltaTime);
            }         
            
        }
	
	}

    void OnTriggerEnter2D() {
		Destroy(this.gameObject);
    }

    
}
