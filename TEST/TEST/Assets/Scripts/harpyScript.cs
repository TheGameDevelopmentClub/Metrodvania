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

    private bool fix = false;
    private Vector2 playerPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Check position of player
        if (player.transform.position.x > (transform.position.x - 2.5f) && player.transform.position.x < (transform.position.x + 2.5f)) {

            playerNear = true;

        }
        else {

            playerNear = false;
        }

        //Iddle moving
        if (!playerNear)
        {

            //Up - Down
            if (transform.position.y > (initialPositionY + 0.2f) || transform.position.y < (initialPositionY - 0.2f))
            {
                direction = direction * -1;
            }
            rigidbody2D.velocity = new Vector2(transform.position.x, (direction * upDownSpeed));

            //Sides movement
            time += Time.deltaTime;

            if (time > 2.5f)
            {
                side = side * -1.0f;
                time = 0;

            }
            rigidbody2D.velocity = new Vector2((side * sidesSpeed), rigidbody2D.velocity.y);
            transform.localScale = new Vector3(-side, 1, 1);

        }
        else {

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) > 1f && !fix)
            {
                fix = true;
                playerPos = player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, playerPos, attSpeed * Time.deltaTime);
            }                 
            
        }
	
	}

    void OnTriggerEnter2D() {
		Destroy(this.gameObject);
    }
}
