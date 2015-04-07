using UnityEngine;
using System.Collections;

public class runParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Set the sorting layer of the particle system.
        particleSystem.renderer.sortingLayerName = "Player";
        particleSystem.renderer.sortingOrder = 4;
	}
	
	// Update is called once per frame
	void Update () {
	
        // Set the sorting layer of the particle system.
        particleSystem.renderer.sortingLayerName = "Player";
        particleSystem.renderer.sortingOrder = 4;
	}
}
