using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliffSpeedBoost : MonoBehaviour {

	public villageManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "fire") {
			moveFire firebar = other.GetComponent<moveFire> ();
			firebar.fireSpeed *= 8.0f;
			//firebar.
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "fire") {
			moveFire firebar = other.GetComponent<moveFire> ();
			firebar.fireSpeed /= 2.5f;
			//firebar.
		}
	}
}
