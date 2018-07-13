using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

	public villageManager manager;

	public Color water_color;
	public Color oil_color;
	public Color default_color;

	public Collider2D player_coll;
	public Rigidbody2D player_rb;

	public SpriteRenderer player_renderer;

	public float walkSpeed;
	public float defaultSpeed;
	public float objWeight;
	public float walkingTimer;

	public bool playerCarryWater;
	public bool playerCarryOil;

	public bool lookingLeft; // if player looking left, this will be true. otherwise false. used for inverting look direction of sprite
	public Transform pickaxe;

	public bool speedBuff;//speed buff bool
	public float buffTime; //bufftimer

    private AudioSource walkingPlayerSound;
    private bool isWalking = false;
    private bool isPlaying = false;

    public bool carryingVillager;
    public double rescuedVillagerTimer = 0.0;

    // Use this for initialization
    void Start () {
		defaultSpeed = 750.0f;
		objWeight = 15.0f;
		playerCarryWater = false;
		playerCarryOil = false;
        walkingPlayerSound = GameObject.Find("WalkingPlayerSound").GetComponent<AudioSource>();
		water_color = new Color32( 0x4B, 0x65, 0xE3, 0xFF ); // RGBA
		oil_color = new Color32( 0x31, 0x30, 0x30, 0xFF );
		default_color = new Color32( 0xC1, 0x70, 0x75, 0xFF );
		player_renderer = gameObject.GetComponent<SpriteRenderer>();
		lookingLeft = false;
		walkingTimer = 0.0f;
    }

	// Update is called once per frame
	void Update () {

		//speedBuff	
		if (buffTime > Time.time) {
			objWeight = 0.0f;
		}
		else {
			objWeight = 15.0f;
		}

//		walkSpeed = defaultSpeed + (manager.minedStones * -objWeight) + 
//			(manager.wallInventory.Count * -objWeight);

		walkSpeed = defaultSpeed;


		Vector2 direction = Vector2.zero;
		if (Input.GetAxisRaw ("DPADHorizontal") != 0.0f) {
			direction = new Vector2 (
				Input.GetAxisRaw ("DPADHorizontal"),
				//Input.GetAxisRaw("Vertical")
				0.0f	// vertical motion is based on grid system
			);

		} else {
			direction = new Vector2 (
				Input.GetAxisRaw ("Horizontal"),
			//Input.GetAxisRaw("Vertical")
				0.0f	// vertical motion is based on grid system
			);
		}

//		if (direction.x < 0 && !lookingLeft) { // if moving left and not looking left
//			lookingLeft = true;
//			pickaxe.localScale = new Vector3 (
//				pickaxe.localScale.x,
//				-pickaxe.localScale.y,
//				pickaxe.localScale.z
//			);
//		} else if (direction.x > 0 && lookingLeft) { // else if moving right and not looking right
//			lookingLeft = false;
//			pickaxe.localScale = new Vector3 (
//				pickaxe.localScale.x,
//				-pickaxe.localScale.y,
//				pickaxe.localScale.z
//			);
//		}

		// if we have any movement at all, make sure to set isWalking to true
		if (direction != Vector2.zero || (Input.GetAxisRaw ("Vertical") != 0.0f) || (Input.GetAxisRaw ("DPADVertical") != 0.0f)) {    // if direction != zero, we need to handle some kind of player movement
			isWalking = true;
		}
        
		// handle horizontal movement
		// Can happen every frame since direction could just be (0.0, 0.0) if player doesn't move
		player_rb.AddForce(direction * walkSpeed);	// simply add a walkSpeed force in the movement direction

		// handle vertical movement
		if (Input.GetAxisRaw ("Vertical") != 0.0f || (Input.GetAxisRaw ("DPADVertical") != 0.0f)) {
			walkingTimer -= Time.deltaTime;
			if (walkingTimer < 0.0f) {
				walkingTimer = 0.3f;
				float walkAmount;
				if (Input.GetAxisRaw ("Vertical") > 0.0f || (Input.GetAxisRaw ("DPADVertical") < 0.0f)) {
					walkAmount = 0.6666f;
				} else {
					walkAmount = -0.6666f;
				}
				Vector2 newPosition = new Vector2 (
					transform.position.x,
					transform.position.y + walkAmount
				);
				// attempts to place player in new grid location
				// returns true if player successfully moved, returns false if player could not be moved
				attemptMovePlayer (newPosition);
			}
		} else if (isWalking) {		// else we have stopped vertical movement, tell system to reset variables and stop playing SFX
			walkingTimer = 0.0f;
			isWalking = false;
			walkingPlayerSound.Stop();
			isPlaying = false; 
		}
		

//			if (Input.GetKey (KeyCode.UpArrow)) {
//				player_rb.AddForce (transform.up * walkSpeed);
//			} else if (Input.GetKey (KeyCode.DownArrow)) {
//				player_rb.AddForce (-transform.up * walkSpeed);
//			}
//
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				player_rb.AddForce (-transform.right * walkSpeed);
//			} else if (Input.GetKey (KeyCode.RightArrow)) {
//				player_rb.AddForce (transform.right * walkSpeed);
//			}

        if (!isPlaying && isWalking)
        {
            walkingPlayerSound.Play();
            isPlaying = true;
        }

        if (carryingVillager && transform.position.x < -1.65)
        {
            carryingVillager = false;
            Instantiate(GameObject.Find("Villager"));
            GameObject.Find("Villager(Clone)").transform.position = new Vector3(-1.7f, -0.029f, transform.position.z);
            
            rescuedVillagerTimer = 20.0;
        }
        if (rescuedVillagerTimer > 0.0)
        {
            GameObject.Find("Villager(Clone)").GetComponent<carryWater>().walkingSpeed = 4.0f;
            rescuedVillagerTimer -= .016;
        }
        else
        {
            Destroy(GameObject.Find("Villager(Clone)"));
        }


	}

	// attempts to put the player in a new grid location for vertical motion, depending on what it does
	bool attemptMovePlayer(Vector2 newPosition) {
		Collider2D result_coll = Physics2D.OverlapCircle (newPosition, 0.1f);

		// if the result_coll not null and is a stone, then grab its moveStone component and attempt to push it
		if (result_coll && result_coll.tag == "stone") {
			
			// figure out which directiont o push the stone
			moveStone stone = result_coll.GetComponent<moveStone> ();
			moveStone.Direction moveDirection;
			if (transform.position.y < result_coll.transform.position.y) {	// if player is lower than stone
				moveDirection = moveStone.Direction.up;		// push stone up
			} else {										// else player is above stone
				moveDirection = moveStone.Direction.down;	// push stone down
			}

			// attempt to push the stone and then the player with it
			if (stone.pushStone (moveDirection)) {	// if stone was pushed successfully
				transform.position = newPosition;	// then push player as well
				return true;
			} else {					// else we could not move the player because
				return false;			// stone is blocked by something (possibly another stone)
			}
		} else if (result_coll && result_coll.tag == "tree"){	// if the only obstacle is a tree, just push through it
			transform.position = newPosition;
			return true;
		} else if (result_coll && (result_coll.tag == "NoPushStone" || result_coll.tag == "NoBuildWood") ){
			transform.position = newPosition;
			return true;
		} else if (!result_coll) {	// else if there is no obstacle blocking a player move
			transform.position = newPosition;	// move player to new position
			return true;
		} else {				// else there is an unknown obstacle in the way, don't even try to move player
			return false;
		}
	}

	void OnCollisionStay2D( Collision2D coll){
		
		if (playerCarryWater) {
			if (coll.gameObject.name == "Bridge Colliders") {
				player_renderer.color = default_color;
				playerCarryWater = false;
				manager.addPlayerBucket ();
			}

		}
		/*
		if (playerCarryOil) {
			if (coll.gameObject.tag == "workbench") {
				buildWall furnace = coll.gameObject.	GetComponent<buildWall>();
				furnace.woodStorage += 1;
				furnace.woodTimer += 15.0f / furnace.woodStorage;
				player_renderer.color = default_color;
				playerCarryOil = false;

			}

		}*/
		else {
			if (coll.gameObject.tag  == "water" ) {//&& !playerCarryOil) {
					player_renderer.color = water_color;
					playerCarryWater = true;
				
			}
		}
			
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        print("Collided! coll: " + coll.gameObject.name);
        if (coll.gameObject.tag == "stranded")
        {
            Debug.Log("help me");
            carryingVillager = true;
            Destroy(coll.gameObject);
            //GameObject.FindGameObjectWithTag("stranded").GetComponent<carryWater>().enabled = true;
        }
    }
	/*
    public void OnTriggerStay2D(Collider2D coll)
	{	/
		print("Trigger coll: " + coll.gameObject.name);
		if (coll.gameObject.tag == "oil" && !playerCarryOil) { //!playerCarryWater && !playerCarryOil) {
			if (Input.GetKeyDown (manager.mineButton)) {
				player_renderer.color = oil_color;
				playerCarryOil = true;
				Destroy (coll.gameObject);
			}
		}

		if (coll.gameObject.tag == "speedbuff") {
			buffTime = Time.time + 10.0f;
		}

	}*/
}