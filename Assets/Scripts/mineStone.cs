using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineStone : MonoBehaviour {
	
	public villageManager manager;

	Animator animate_Player;
	//Animator animate_Player2;

	public bool mineablePlayer1;		// player 1 currently within mining distance
	//public bool mineablePlayer2;		// player 2 currently within mining distance
	public bool hasBeenMined;	// stone has been mined out by player already

	public float mineTimer;

	public GameObject oilSpill;

	public GameObject player;
	//public GameObject player2;

	public SpriteRenderer stone_renderer;

	public GameObject plusOne; 		// "+1" sprite that appears over player's head every time they mine a stone

    private AudioSource mineStoneSound;
    private AudioSource oilSpillSound;

	// Use this for initialization
	void Start () {
		mineablePlayer1 = false;
		//mineablePlayer2 = false;
		hasBeenMined = false;
		mineTimer = 1.0f;
		oilSpill = GameObject.Find ("Oil Spill");
        mineStoneSound = GameObject.Find("MineStoneSound").GetComponent<AudioSource>();
        oilSpillSound = GameObject.Find("OilSpillSound").GetComponent<AudioSource>();
		player =  GameObject.Find("Player 1");
		//player2 =  GameObject.Find("Player 2");
		animate_Player = player.GetComponent<Animator>();
		//animate_Player2 = player2.GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {

		if (!(hasBeenMined) && mineablePlayer1) {
			if (Input.GetKey(manager.mineButton)) {
				animate_Player.SetBool ("Mine", true);
                
				mineTimer -= Time.deltaTime;
				stone_renderer.color = new Color (
					stone_renderer.color.r,
					stone_renderer.color.g,
					stone_renderer.color.b,
					mineTimer				// set alpha to whatever's left on mineTimer
				);
				if (mineTimer <= 0.0f) {
                    mineStoneSound.Play();
                    hasBeenMined = true;
					animate_Player.SetBool ("Mine", false);
					manager.minedStones += 1;
					manager.generateStone ();
					/*if (manager.oilSpillLikeliness > 1) {
						manager.oilSpillLikeliness -= 1;
					}
					if (Random.Range (0, manager.oilSpillLikeliness) == 0) {
						spillOil ();
					}*/
					if (Random.Range (0, 10) == 0) {
					spillOil ();
					}
				}
			}
		}
		/*
		if (!(hasBeenMined) && mineablePlayer2) {
			if (Input.GetKey(manager.mineButton2)) {
				animate_Player2.SetBool ("Mine", true);

				mineTimer -= Time.deltaTime;
				stone_renderer.color = new Color (
					stone_renderer.color.r,
					stone_renderer.color.g,
					stone_renderer.color.b,
					mineTimer				// set alpha to whatever's left on mineTimer
				);
				if (mineTimer <= 0.0f) {
					mineStoneSound.Play();
					StartCoroutine ("DrawPlusOne");
					hasBeenMined = true;
					animate_Player2.SetBool ("Mine", false);
					manager.minedStones += 1;
					manager.generateStone ();
					/*if (manager.oilSpillLikeliness > 1) {
						manager.oilSpillLikeliness -= 1;
					}
					if (Random.Range (0, manager.oilSpillLikeliness) == 0) {
						spillOil ();
					}*/
		/*
					if (Random.Range (0, 10) == 0) {
						spillOil ();
					}
				}
			}
		}*/

	}

	void OnTriggerEnter2D(Collider2D other){
		if (!(hasBeenMined) && other.tag == "Player") {
			mineablePlayer1 = true;
		}
	/*
		if (!(hasBeenMined) && other.tag == "Player2") {
			mineablePlayer2 = true;
		}
		*/
	}

	void OnTriggerExit2D (Collider2D other){
		if (!(hasBeenMined) && other.tag == "Player") {
			mineablePlayer1 = false;
			animate_Player.SetBool ("Mine", false);

		}
	/*
		if (!(hasBeenMined) && other.tag == "Player2") {
			mineablePlayer2 = false;
			animate_Player2.SetBool ("Mine", false);

		}
		*/
	}

	void spillOil () {
        oilSpillSound.Play();
        GameObject newOil = Instantiate (oilSpill);
		newOil.transform.position = transform.position;
		newOil.transform.Rotate(Vector3.forward, Random.Range(-180.0f, 180.0f));
		newOil.AddComponent<spillOil> ();
	}

	IEnumerator DrawPlusOne () {
		GameObject newPlusOne = Instantiate (plusOne);
		SpriteRenderer renderer = newPlusOne.GetComponent<SpriteRenderer> ();
		newPlusOne.transform.position = transform.position;
		float origin_y = newPlusOne.transform.position.y + 0.3f;
		float animateTimer = 1.0f;
		while (animateTimer > 0.0f) {
			animateTimer -= Time.deltaTime;
			renderer.color = new Color (
				renderer.color.r,
				renderer.color.g,
				renderer.color.b,
				animateTimer
			);
			newPlusOne.transform.position = new Vector3 (
				newPlusOne.transform.position.x,
				origin_y + 1.0f * (1 - animateTimer), // translates in +y direction, getting higher as timer gets lower
				newPlusOne.transform.position.z
			);
			yield return null;
		}
		Destroy (newPlusOne);
	}
}
