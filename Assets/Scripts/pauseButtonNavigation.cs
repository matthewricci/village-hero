using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtonNavigation : MonoBehaviour {

	public PauseMenu pauseMenu;

	int index = 0;
	public int totalButtons = 3;
	public float yOffset = 0.9f;

	public bool hasBeenPressed;

	// Use this for initialization
	void Start () {
		hasBeenPressed = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) || (Input.GetAxisRaw("DPADVertical") > 0 && !hasBeenPressed)) {
			hasBeenPressed = true;
			if (index < totalButtons - 1) {
				index++;
				Vector2 position = transform.position;
				position.y -= yOffset;
				transform.position = position;
			}
		}

		if (Input.GetKeyDown (KeyCode.W) || (Input.GetAxisRaw("DPADVertical") < 0 && !hasBeenPressed)) {
			hasBeenPressed = true;
			if (index > 0) {
				index--;
				Vector2 position = transform.position;
				position.y += yOffset;
				transform.position = position;
			}
		}

		if (Input.GetAxisRaw ("DPADVertical") == 0) {
			hasBeenPressed = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Joystick1Button0)) {
			if (index == 0) {
				pauseMenu.Resume();
			}
			else if (index == 1) {
				pauseMenu.Resume();
				SceneManager.LoadScene ("Level 0");
			}
			else if (index == 2) {
				Application.Quit ();
			}
		}
	}
}