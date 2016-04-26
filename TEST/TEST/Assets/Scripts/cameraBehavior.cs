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
	private Camera camera;
    // Use this for initialization
    void Start() {
        camera = Camera.main;        

    }

    // Update is called once per frame
    void Update()
    {
		
        //First Room
        if (player.transform.position.x > 658.77f && player.transform.position.x < 673.82f && player.transform.position.y > -869.73f && player.transform.position.y < -865.06f)
        {
			camera.fieldOfView = 3.8f;
        }
        else if (player.transform.position.x > 669f && player.transform.position.x < 673.84f && player.transform.position.y > -878.36f && player.transform.position.y < -869.73f)
        {
			camera.fieldOfView = 1.57f;
			camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10f), Time.deltaTime*1.4f);
        }
    }
}
      /*if (player.transform.position.x > (this.transform.position.x + 6.05f) || keepRight) {

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
        }*/