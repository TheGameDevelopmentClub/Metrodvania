using UnityEngine;
using System.Collections;

public class turretScript : MonoBehaviour {

    public GameObject turretProjectile;
    public float speed;
    public float delay;
	public int side;

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
            clone.rigidbody2D.velocity = (side * transform.right) * speed;
        }
    }
}
