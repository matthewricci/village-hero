using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseMenu : MonoBehaviour {

	public bool LevelIsLost;

	public GameObject loseMenuUI;

	public villageManager Manager;

	// Use this for initialization
	void Start () {
		LevelIsLost = false;
	}

	// Update is called once per frame
	void Update () {
		if (Manager.gameOn == false) {
			if (Manager.winner == false) {
				Time.timeScale = 0f;
				loseMenuUI.SetActive (true);
			}
		}
	}

	public void Restart () {
		loseMenuUI.SetActive (false);
		Time.timeScale = 1f;
	} 
}