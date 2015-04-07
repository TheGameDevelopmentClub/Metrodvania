using UnityEngine;
using System.Collections;

public class turretProjectile : MonoBehaviour {

    public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Destroyed when hit something alse than turret barrel
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "turret" && other.tag != "deadly")
        {
            
            DestroyIt();
        }
    }

    //Destroy fancy
    void DestroyIt() {

        collider2D.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 2f);

    }
}
