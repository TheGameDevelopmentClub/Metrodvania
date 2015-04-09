using UnityEngine;
using System.Collections;

public class turretScript : MonoBehaviour {

    public GameObject turretProjectile;
    public float speed;
    public float delay;
	public bool faceLeft;

	// Use this for initialization
	void Start () {

        StartCoroutine( Shoot() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Shoot() {

        while (true) {

            yield return new WaitForSeconds(delay);
            GameObject clone = (GameObject) Instantiate(turretProjectile, transform.position, Quaternion.identity);
			if (faceLeft) clone.rigidbody2D.velocity = -transform.right * speed;
			else clone.rigidbody2D.velocity = transform.right * speed;
        }
    }
}
