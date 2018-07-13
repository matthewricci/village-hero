using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class buildWall : MonoBehaviour {

	public villageManager manager;
	public GameObject player;
	public GameObject player2;

	public bool buildablePlayer1;
	public bool buildablePlayer2;

	public SpriteRenderer smelter_hole;
	public GameObject wallArchetype;

	public Color idle;
	public Color maxHeat;

	int lerpDirection;
	float lerptime;

	int stoneStorage;
	public int woodStorage;
	int wallStorage;

	public Text furnaceText;

    private AudioSource buildWallSound;
    private AudioSource furnaceSmokeSound;

    public ParticleSystem smokeEffect;

	float timeNow;
	float timeDepositStone;
	float timeDepositWood;
	public float woodTimer;
    // Use this for initialization
    void Start () {
		stoneStorage = 0;
		woodStorage = 0;
		wallStorage = 0;
		buildablePlayer1 = false;
		buildablePlayer2 = false;
		lerpDirection = 1;
		lerptime = 0.0f;
		woodTimer = 0.0f;
        buildWallSound = GameObject.Find("BuildWallSound").GetComponent<AudioSource>();
        furnaceSmokeSound = GameObject.Find("FurnaceSmokeSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		if (woodTimer > 0.0f) {
			woodTimer -= Time.deltaTime;
		}

		//smokeEffect.Play ();
//		furnaceText.text = "" + wallStorage;//"Furnace: " + wallStorage;
		if (buildablePlayer1 && manager.minedStones > 0) {

			// start code for player 1 storing stones for building
			if (Input.GetKeyDown (manager.mineButton)) {
				//smokeEffect.Play ();
				stoneStorage += 1;
				manager.minedStones -= 1;
				timeDepositStone = Time.time;
				/*lerptime += Time.deltaTime;
				smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
				if (lerptime > 1.0f) {
					smelter_hole.color = idle;
					lerptime = 0.0f;
					//stoneStorage -= 1;
					manager.minedStones -= 1;
					wallStorage += 1;
					//createWall ();
				}*/
			}

			if (Input.GetKey (manager.mineButton) && (Time.time >= (timeDepositStone + 1.0f))) {
				//smokeEffect.Play ();
				stoneStorage += manager.minedStones;
				manager.minedStones = 0;
				/*lerptime += Time.deltaTime;
				smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
				if (lerptime > 1.0f) {
					smelter_hole.color = idle;
					lerptime = 0.0f;
					//stoneStorage -= 1;
					manager.minedStones -= 1;
					wallStorage += 1;
					//createWall ();
				}*/
			} /*else if (Input.GetKeyUp (manager.mineButton)) {
				smelter_hole.color = idle;
				lerptime = 0.0f;
			}*/
		}
			
		//Player2 Give Wood to furnace
		if (buildablePlayer2 && manager.choppedTrees > 0) {

			// start code for player 1 storing stones for building
			if (Input.GetKeyDown (manager.mineButton2)) {
				woodStorage += 1;
				woodTimer += 15.0f / woodStorage;
				manager.choppedTrees -= 1;
				timeDepositWood = Time.time;
				/*lerptime += Time.deltaTime;
				smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
				if (lerptime > 1.0f) {
					smelter_hole.color = idle;
					lerptime = 0.0f;
					//stoneStorage -= 1;
					manager.minedStones -= 1;
					wallStorage += 1;
					//createWall ();
				}*/
			}

			if (Input.GetKey (manager.mineButton2) && (Time.time >= (timeDepositWood + 1.0f))) {
				woodStorage += manager.choppedTrees;
				woodTimer += 5.0f / woodStorage;
				manager.choppedTrees = 0;
				/*lerptime += Time.deltaTime;
				smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
				if (lerptime > 1.0f) {
					smelter_hole.color = idle;
					lerptime = 0.0f;
					//stoneStorage -= 1;
					manager.minedStones -= 1;
					wallStorage += 1;
					//createWall ();
				}*/
			} /*else if (Input.GetKeyUp (manager.mineButton)) {
				smelter_hole.color = idle;
				lerptime = 0.0f;
			}*/
		}

		// start code for player 1 grabbing walls from storage
		if (buildablePlayer1 && wallStorage > 0) {
			if (Input.GetKeyDown (manager.buildButton)) {
				timeNow = Time.time;
				createWall (player);
			} 
			if (Input.GetKey (manager.buildButton) && (Time.time >= (timeNow + 1.0f))) {
				createWall (player);
			}
		}
		// start code for player 2 grabbing walls from storage
		if (buildablePlayer2 && wallStorage > 0) {
			if (Input.GetKeyDown (manager.buildButton2)) {
				timeNow = Time.time;
				createWall (player2);
			} 
			if (Input.GetKey (manager.buildButton) && (Time.time >= (timeNow + 1.0f))) {
				createWall (player2);
			}
		}

		if (stoneStorage > 0 && woodTimer > 0.0f) {
			//smokeEffect.Play ();	
			lerptime += Time.deltaTime;
			smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
			if (lerptime > 1.0f) {
				smelter_hole.color = idle;
				lerptime = 0.0f;
				stoneStorage -= 1;
				woodStorage -= 1;
				wallStorage += 1;
                furnaceSmokeSound.Play();
				//createWall ();
			}
		}

		if (woodTimer > 0.0f && !smokeEffect.isPlaying) {
			smokeEffect.Play ();
		}
		if (woodTimer <= 0.0f && smokeEffect.isPlaying) {
			smokeEffect.Stop ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			buildablePlayer1 = true;
		}
		if (other.tag == "Player2") {
			buildablePlayer2 = true;
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			buildablePlayer1 = false;
		}
		if (other.tag == "Player2") {
			buildablePlayer2 = false;
		}
	}

	void createWall(GameObject player){
        buildWallSound.Play();
        GameObject newWall = Instantiate (wallArchetype, player.transform);
		//newWall.transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		newWall.transform.position = new Vector3 (
			player.transform.position.x + 0.5f,
			player.transform.position.y,
			player.transform.position.z
		);
		wallStorage -= 1;
		if (player.tag == "Player") {
			manager.wallInventory.Add (newWall);
		} else {
			manager.wallInventory2.Add (newWall);
		}
	}
}
