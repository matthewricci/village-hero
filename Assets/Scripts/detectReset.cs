using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class detectReset : MonoBehaviour {

	public float player1HoldTimer;		// hold long player has held the reset button for
	public float player2HoldTimer;

	// Use this for initialization
	void Start () {
		player1HoldTimer = 0.0f;
		player2HoldTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Joystick1Button7)) {
			player1HoldTimer += Time.deltaTime;
			if (player1HoldTimer > 1.0f) {
				SceneManager.LoadScene ("Level 0");
			}
		} else {
			player1HoldTimer = 0.0f;
		}

		if (Input.GetKey (KeyCode.Joystick2Button7)) {
			player2HoldTimer += Time.deltaTime;
			SceneManager.LoadScene ("Level 0");
		} else {
			player2HoldTimer = 0.0f;
		}

	}
}
