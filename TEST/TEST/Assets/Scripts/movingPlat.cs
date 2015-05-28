using UnityEngine;
using System.Collections;

public class movingPlat : MonoBehaviour {

	public float moveAmount = 1f;
	public bool back = false;
    public bool move = false;
    public float limit;
    public float start;

	// Use this for initialization
	void Start () {
        limit = this.transform.position.x - 5f;
        start = this.transform.position.x;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (move) { 
            if(!back) {

                if (this.transform.position.x > limit+0.05f)  {

                    this.rigidbody2D.velocity = new Vector2(-moveAmount, 0f);
                }
                else if (this.transform.position.x >= limit && this.transform.position.x <= limit+0.05f)
                {
                    this.rigidbody2D.velocity = new Vector2(0f, 0f);
                    back = true;
                }
            }

            else {
                if (this.transform.position.x < start-0.05f)
                {

                    this.rigidbody2D.velocity = new Vector2(moveAmount, 0f);
                }
                else if (this.transform.position.x <= start && this.transform.position.x >= start-0.05f) {
                    this.rigidbody2D.velocity = new Vector2(0f, 0f);
                    this.transform.position = new Vector2(start, this.transform.position.y);
                    back = false;
                    move = false;
                }

            }

        }

	}

	public void Toggle () {

        move = true;
	}
}
