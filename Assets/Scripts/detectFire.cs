using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectFire : MonoBehaviour {

	public villageManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (manager.gameOn) {
			if (other.tag == "fire") {
				manager.gameOn = false;
				manager.winner = false;
				other.GetComponent<moveFire> ().fireSpeed = 1.0f;
			}
		} else if (manager.winner) {
			if (other.tag == "fire") {
				other.GetComponent<moveFire> ().stopped = true;
				other.GetComponent<moveFire> ().fireSpeed = 0.0f;

			}
		} else {
			if (other.tag == "fire") {
				other.GetComponent<moveFire> ().fireSpeed = 1.0f;
			}
		}
	}
}
