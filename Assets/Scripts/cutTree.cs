using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutTree : MonoBehaviour {

	public villageManager manager;

	Animator animate_Player;
	//Animator animate_Player2;

	public bool mineablePlayer1;		// player 1 currently within mining distance
	//public bool mineablePlayer2;		// player 2 currently within mining distance
	//public bool hasBeenMined;	// stone has been mined out by player already
	public bool hasBeenCut;	// stone has been mined out by player already

	public float mineTimer;
	public float cutTimer;

	public GameObject player;
	public GameObject wallArchetype;
	//public GameObject player2;


	public List<SpriteRenderer> treeSprites;

	private AudioSource buildWallSound;
    private AudioSource cutTreeSound;

	float cutTime;

	float burnTime;

	bool burning;

	public ParticleSystem smoke;

	public GameObject Axe;

    public GameObject plusOne; 		// "+1" sprite that appears over player's head every time they mine a stone

	//private AudioSource mineStoneSound;
	//private AudioSource oilSpillSound;

	// Use this for initialization
	void Start () {
		mineablePlayer1 = false;
		//mineablePlayer2 = false;
		//hasBeenMined = false;
		//mineTimer = 0.5f;
		hasBeenCut = false;
		cutTimer = 0.5f;
		//oilSpill = GameObject.Find ("Oil Spill");
		//mineStoneSound = GameObject.Find("MineStoneSound").GetComponent<AudioSource>();
		//oilSpillSound = GameObject.Find("OilSpillSound").GetComponent<AudioSource>();
		player =  GameObject.Find("Player 1");
		//player2 =  GameObject.Find("Player 2");
		animate_Player = player.GetComponent<Animator>();
		//animate_Player2 = player2.GetComponent<Animator> ();

		buildWallSound = GameObject.Find("BuildWallSound").GetComponent<AudioSource>();

		// get all stone sprite children and insert into stoneSprites list
		GetComponentsInChildren<SpriteRenderer> (treeSprites);

		burning = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (!(hasBeenCut) && mineablePlayer1 && (manager.hasWood == false)) {
				Axe.SetActive (true);
				animate_Player.SetBool ("Mine", true);
				cutTimer -= Time.deltaTime;
				if (cutTimer <= 0.0f) { // if it is time to delete a sprite from the stone prefab
                                        //mineStoneSound.Play();
                    //cutTreeSound.Play();
                    //StartCoroutine ("DrawPlusOne");
					manager.hasWood = true;	// add a stone every time a rock is mined, 3 total per stone prefab
					// if we are not on the last sprite of the prefab
						//Destroy (treeSprites [treeSprites.Count - 1].gameObject);
						//treeSprites.RemoveAt (treeSprites.Count - 1);
						//treeSprites [treeSprites.Count - 1].enabled = false;
				if (treeSprites.Count == 2) {
					treeSprites [treeSprites.Count - 1].enabled = false;
				} else {
					treeSprites [treeSprites.Count - 1].enabled = false;
					treeSprites [treeSprites.Count - 2].enabled = false;
					treeSprites [treeSprites.Count - 3].enabled = false;

				}
						cutTimer = 0.5f;
						print("last one");
						hasBeenCut = true;
						mineablePlayer1 = false;
						animate_Player.SetBool ("Mine", false);
						Axe.SetActive (false);
						createWall (player);
						cutTime = Time.time + 15.0f;


					 /*else {						// else we just mined the last sprite, time to delete the object
						print("last one");
						hasBeenCut = true;
						animate_Player2.SetBool ("Mine", false);
						//manager.generateStone ();
						Destroy (treeSprites [treeSprites.Count - 1].gameObject);
						treeSprites.RemoveAt (treeSprites.Count - 1);

					}*/
				}
			
		}
		if (hasBeenCut) {
			if (treeSprites.Count == 2) {
				if (Time.time >= cutTime) { 
					hasBeenCut = false;
					treeSprites [1].enabled = true;
				}
			}
			if (treeSprites.Count > 2) {
				if (Time.time >= cutTime) { 
					hasBeenCut = false;
					treeSprites [treeSprites.Count - 1].enabled = true;
				}
				else if (Time.time >= cutTime - 5.0f) { 
					treeSprites [treeSprites.Count - 2].enabled = true;
				}
				else if (Time.time >= cutTime - 10.0f) { 
					treeSprites [treeSprites.Count - 3].enabled = true;
				}
			}


		}
		if (burning) {
			if (Time.time >= burnTime) {
				Destroy (gameObject);
			}
		}

	}
	void burnTree (){
		Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D other){
		if (!(hasBeenCut) && other.tag == "Player") {
			mineablePlayer1 = true;
		}
		if (other.tag == "fire") {
			smoke.Play ();
			burning = true;
			burnTime = Time.time + 3.0f;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (!(hasBeenCut) && other.tag == "Player") {
			cutTimer = 0.5f;
			mineablePlayer1 = false;
			animate_Player.SetBool ("Mine", false);
			Axe.SetActive (false);
		}
	}

	/*
	void spillOil () {
		oilSpillSound.Play();
		GameObject newOil = Instantiate (oilSpill);
		newOil.transform.position = transform.position;
		newOil.transform.Rotate(Vector3.forward, Random.Range(-180.0f, 180.0f));
		newOil.AddComponent<spillOil> ();
	}*/

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

	void createWall(GameObject player){
		buildWallSound.Play();
		GameObject newWall = Instantiate (wallArchetype, player.transform, true);
		//newWall.transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		newWall.transform.position = new Vector3 (
			player.transform.position.x + 0.8f,
			player.transform.position.y,
			player.transform.position.z
		);
		newWall.GetComponent<placeWood> ().heldByPlayer = true;	// grab placeWood script on plank and set bool to true
	}
}
