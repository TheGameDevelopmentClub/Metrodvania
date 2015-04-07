using UnityEngine;
using System.Collections;

public class sprits : MonoBehaviour {

	public string spriteName;
	public Sprite[] sprites;
	public int ourSprite;

	// Use this for initialization
	void Start () {

		sprites = Resources.LoadAll<Sprite>(spriteName);
		ourSprite = Random.Range(0, sprites.Length);
		GetComponent<SpriteRenderer>().sprite = sprites[ourSprite];

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
