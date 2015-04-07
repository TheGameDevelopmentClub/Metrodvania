using UnityEngine;
using System.Collections;

public class movingGrass : MonoBehaviour {

	public float moveAmount = 1f;
	public bool gone;

	// Use this for initialization
	void Start () {
		gone = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (rigidbody2D.velocity.y != 0) {

			Vector2 v = rigidbody2D.velocity;
			v.y = 0;
			rigidbody2D.velocity = v;
		}

	}

	public void Toggle (bool trigger) {

		if (trigger) {

			if (!gone) {
				rigidbody2D.velocity = new Vector2 (-moveAmount, 0);
			} else {
				rigidbody2D.velocity = new Vector2 (moveAmount, 0);
			}
		}
	}
}
