using UnityEngine;
using System.Collections;

public class particleDestry : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Set the sorting layer of the particle system.
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 4;

        Destroy(this.gameObject, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
