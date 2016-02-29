using UnityEngine;
using System.Collections;

public class runParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Set the sorting layer of the particle system.
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 4;
	}
	
	// Update is called once per frame
	void Update () {
	
        // Set the sorting layer of the particle system.
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 4;
	}
}
