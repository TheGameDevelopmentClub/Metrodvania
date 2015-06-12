using UnityEngine;
using System.Collections;

public class cameraBehavior : MonoBehaviour {

    //Player
    public GameObject player;

    //Smoothness
    public float smooth = 2.5f;
    private Vector3 pos;
    private Vector3 finalPos;
    public bool keepRight = false;
    private bool keepLeft = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.x > (this.transform.position.x + 6.05f) || keepRight) {

            //Only get in the IF if is the first time that makes this "CLICLE"
            if (!keepRight) { 
                pos = this.transform.position;
                finalPos = new Vector3((pos.x + 12.11f), pos.y, -10f);
            }
            keepRight = true;

            //Move the camera "slowly" to the destination wished 
            this.transform.position = Vector3.Lerp(this.transform.position, finalPos , Time.deltaTime * smooth);

            //if gets closer to the final destination gets out
            if (this.transform.position.x <= (finalPos.x + 0.1f) && this.transform.position.x >= (finalPos.x - 0.1f))
            {
                keepRight = false;
            }
        }
        else if (player.transform.position.x < (this.transform.position.x - 6.05f) || keepLeft) {

            //Only get in the IF if is the first time that makes this "CLICLE"
            if (!keepLeft)
            {
                pos = this.transform.position;
                finalPos = new Vector3((pos.x - 12.11f), pos.y, -10f);
            }
            keepLeft = true;

            //Move the camera "slowly" to the destination wished 
            this.transform.position = Vector3.Lerp(this.transform.position, finalPos, Time.deltaTime * smooth);

            //if gets closer to the final destination gets out
            if (this.transform.position.x <= (finalPos.x + 0.1f) && this.transform.position.x >= (finalPos.x - 0.1f))
            {
                keepLeft = false;
            }
        }
	}
}
