using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeWall : MonoBehaviour {

	public villageManager manager;
	public villageManager.playertype playerNum;	// SET THIS IN THE INSPECTOR

    private AudioSource placeWallSound;

	// Use this for initialization
	void Start () {
		placeWallSound = GameObject.Find("PlaceWallSound").GetComponent<AudioSource>();

		// at the start, figure out which player this script is attached to
		playerNum = villageManager.playertype.player1;
		/*
		if (gameObject.tag == "Player") {
			playerNum = villageManager.playertype.player1;
		} else {
			playerNum = villageManager.playertype.player2;
		}*/
    }
	
	// Update is called once per frame
	void Update () {
		//if (playerNum == villageManager.playertype.player1) {	// if attached to player 1
			if (Input.GetKeyDown (manager.buildButton)) {	// constantly check if build button is being pressed
				/*
				if ((manager.wallInventory.Count > 0) && (transform.position.x >= -0.46f)) {
					placeWallSound.Play ();
					manager.wallInventory [manager.wallInventory.Count - 1].transform.parent = null;
					//manager.wallInventory [manager.wallInventory.Count-1].transform.position = transform.position;
					//manager.wallInventory [manager.wallInventory.Count-1].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
					manager.wallInventory.Remove (manager.wallInventory [manager.wallInventory.Count - 1]);
				}
				*/
			if (manager.hasWood == true && (transform.position.x >= -3.00f)) {
					placeWallSound.Play ();
					manager.wallInventory [manager.wallInventory.Count - 1].transform.parent = null;
				manager.wallInventory [manager.wallInventory.Count - 1].GetComponent<Collider>().isTrigger = false;
				manager.wallInventory [manager.wallInventory.Count - 1].
				transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
					//manager.wallInventory [manager.wallInventory.Count-1].transform.position = transform.position;
					//manager.wallInventory [manager.wallInventory.Count-1].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
					manager.wallInventory.Remove (manager.wallInventory [manager.wallInventory.Count - 1]);
					manager.hasWood = false;
				}
			}
		/*
		} else {	// else we are attached to player 2
			if (Input.GetKeyDown (manager.buildButton2)) {	// constantly check if build button is being pressed

				if ((manager.wallInventory2.Count > 0) && (transform.position.x >= -0.46f)) {
					placeWallSound.Play ();
					manager.wallInventory2 [manager.wallInventory2.Count - 1].transform.parent = null;
					//manager.wallInventory2 [manager.wallInventory2.Count-1].transform.position = transform.position;
					//manager.wallInventory2 [manager.wallInventory2.Count-1].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
					manager.wallInventory2.Remove (manager.wallInventory2 [manager.wallInventory2.Count - 1]);
				}
			}
		}*/

	}
}
