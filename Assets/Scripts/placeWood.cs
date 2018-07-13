using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeWood : MonoBehaviour {

	public villageManager manager;

	public bool heldByPlayer;
	public bool woodPlaceable; // true unless the wood the player is holding is in an unplaceable area

	public SpriteRenderer woodSprite;

	private AudioSource placeWallSound;

	public float aliveTimer;

	public List<moveFire> touchingFireBars;

	public ParticleSystem smokeEffect;

	// Use this for initialization
	void Start () {
		placeWallSound = GameObject.Find("PlaceWallSound").GetComponent<AudioSource>();
		aliveTimer = 0.0f;
		woodPlaceable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x >= -3.0f) {
			if (heldByPlayer && (Input.GetKeyDown (manager.buildButton) || (Input.GetKeyDown (KeyCode.Joystick1Button0)))) {
				putWoodDown ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag == "fire") {
			if (heldByPlayer) {
				putWoodDown ();
			}
		
			moveFire firebar = coll.gameObject.GetComponent<moveFire> ();
			firebar.stopped = true;
			firebar.flickAnim.pauseFlicker = true;
			StartCoroutine ("BurnWood");
		}
	}

	void OnCollisionStay2D (Collision2D coll){
		if (coll.gameObject.tag == "fire"){
			aliveTimer -= Time.deltaTime;
			smokeEffect.Play ();
		}

	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "NoBuildWood") {
			print ("wood not placeable!");
			woodPlaceable = false;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "NoBuildWood") {
			woodPlaceable = true;
		}
	}

	void putWoodDown(){
		if (heldByPlayer && woodPlaceable) {
			placeWallSound.Play ();
			heldByPlayer = false;
			transform.parent = null;
			manager.hasWood = false;
			Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D> ();
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.mass = 100000.0f;
		}
	}

	IEnumerator BurnWood(){
		float aliveTimer = 0.0f;
		Color normalWood = woodSprite.color;
		Color burntWood = new Color (
			0.227f,
			0.169f,
			0.169f
		);
		while (aliveTimer < 10.0f) {
			aliveTimer += Time.deltaTime;
			woodSprite.color = Color.Lerp (normalWood, burntWood, aliveTimer / 10.0f);
			yield return null;
		}
		for (int i = 0; i < touchingFireBars.Count; i++) {
			touchingFireBars [i].stopped = false;
			touchingFireBars [i].fireSpeed = 0.1696f;
			touchingFireBars [i].flickAnim.enabled = true;
			touchingFireBars [i].flickAnim.pauseFlicker = false;
		}
		Destroy (gameObject);
	}
}
