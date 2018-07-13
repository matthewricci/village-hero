using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "fire") {
			print ("slow");
			moveFire fireStats = other.GetComponent<moveFire> ();
			fireStats.fireSpeed = fireStats.fireSpeed * 2;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "fire") {
			print ("regain");
			moveFire fireStats = other.GetComponent<moveFire> ();
			fireStats.fireSpeed = fireStats.fireSpeed / 2;
		}
	}
}
