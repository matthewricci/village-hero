using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowFire : MonoBehaviour {

	public villageManager manager;

	public SpriteRenderer borderSprite;

	public float lifeTime;
	public float secAlive;

	public bool onFire;
	bool saveTime;

	// Use this for initialization
	void Start () {
		onFire = false;
		saveTime = true;
		secAlive = 10.0f;
		//lifeTime = Time.time + stayAliveFor;
	}
	
	// Update is called once per frame
	void Update () {
		if (onFire) {
			if (saveTime) {
				lifeTime = Time.time + secAlive;
				saveTime = false;
			}
			if (Time.time >= lifeTime) {
				Destroy (gameObject);
			}
		}

	}

	/*
	void OnCollisionEnter2D(Collider2D other) {
		if (other.tag == "fire") {
			print ("slow");
			moveFire fireStats = other.GetComponent<moveFire> ();
			fireStats.fireSpeed = 0.0f;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "fire") {
			print ("regain");
			moveFire fireStats = other.GetComponent<moveFire> ();
			fireStats.fireSpeed = fireStats.fireSpeed * 2;
		}
	}*/
	public IEnumerator HeatStoneAnimation() {
		float heatTimer = 0.0f;	
		Color noHeatColor = borderSprite.color;
		Color heatedColor = new Color (
			0.588f,
			0.329f,
			0.0f
		);
		while (heatTimer < 2.0f) {
			heatTimer += Time.deltaTime;
			borderSprite.color = Color.Lerp (noHeatColor, heatedColor, heatTimer / 2.0f);
			yield return null;
		}
	}
}
