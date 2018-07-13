using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public bool GameIsPaused;

	public villageManager manager;

	public GameObject pauseMenuUI;

	// Use this for initialization
	void Start () {
		GameIsPaused = false;
	}

	// Update is called once per frame
	void Update () {
		if (manager.gameOn) {
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7)) {
				if (GameIsPaused) {
					Resume ();
				} else {
					Pause ();
				}
			}
		}
	}

	public void Resume () {
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause () {
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}