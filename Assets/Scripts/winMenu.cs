using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winMenu : MonoBehaviour {

	public bool LevelIsWon;

	public GameObject winMenuUI;

	public villageManager Manager;

	// Use this for initialization
	void Start () {
		LevelIsWon = false;
	}

	// Update is called once per frame
	void Update () {
		if (Manager.gameOn == false) {
			if (Manager.winner == true) {
				Next ();
			}
		}
	}

	public void Next () {
		winMenuUI.SetActive (true);
		Time.timeScale = 0f;
	}

	public void Restart () {
		winMenuUI.SetActive (false);
		Time.timeScale = 1f;
	}
}