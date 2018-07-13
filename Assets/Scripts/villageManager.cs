using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class villageManager : MonoBehaviour {

	public bool gameOn;
	public bool winner;
	public int numPlayersAlive;	// decreases by 1 every time a player burns to death. when 0, game over

	public float waterFillAmount;
	public float waterFillRate;

	public Image waterLeft;
	public Image waterRight;

	public int minedStones;
	public int choppedTrees;//trees]

	public bool hasWood;
	public bool woodUnplaceable; // false unless the wood the player is holding is in an unplaceable area

	public int oilSpillLikeliness; // higher makes it LESS likely that a mine will trigger a spill

	public List<GameObject> wallInventory;	// inventory for player 1
	public List<GameObject> wallInventory2; // inventory for player 2

	public GameObject firePos;

	public GameObject stones;
	public GameObject trees;//trees

	public Text stoneText;
	public Text wallText;
	public Text TreeText;
	public Text wallTextP2;


	public KeyCode mineButton;
	public KeyCode buildButton;
	public KeyCode dropButton;
	public KeyCode mineButton2;
	public KeyCode buildButton2;
	public KeyCode dropButton2;

    public float currentHealth;
    public float maxHealth;
    public bool burning;

    public Slider healthBar;
	Scene currentScene;

    private AudioSource winMusic;
    private AudioSource lossMusic;

    float timer;

	public enum playertype{
		player1,
		player2
	};

	// public ParticleSystem smokeEffect;

	// Use this for initialization
	void Start () {
		gameOn = true;
		winner = false;
		numPlayersAlive = 1;
		timer = 0.0f;
		waterFillAmount = 0.0f;
		waterLeft.fillAmount = 0.0f;
		waterRight.fillAmount = 0.0f;
		minedStones = 0;
		choppedTrees = 0; //trees
		hasWood = false;
//		mineButton = KeyCode.Space;
		buildButton = KeyCode.Space;
//		dropButton = KeyCode.D;
//		mineButton = KeyCode.Joystick1Button0;
//		buildButton = KeyCode.Joystick1Button1;
		dropButton = KeyCode.Joystick1Button2;
		mineButton2 = KeyCode.Joystick2Button0;
		buildButton2 = KeyCode.Joystick2Button1;
		dropButton2 = KeyCode.Joystick2Button2;
		oilSpillLikeliness = 30;
        maxHealth = 20f;
        currentHealth = maxHealth;
        burning = false;
        healthBar.value = calculateHealth();
        winMusic = GameObject.Find("WinMusic").GetComponent<AudioSource>();
        lossMusic = GameObject.Find("LossMusic").GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene ();
	}

	// Update is called once per frame
	void Update () {
		
		if (!(gameOn)) {
			//timer += Time.deltaTime;
			//if (timer > 2.0f) {
				if (winner) {
                //SceneManager.LoadScene ("Level 4 (Win)");
                winMusic.enabled=true;
                GameObject.Find("BurningPlayerSound").GetComponent<AudioSource>().enabled = false;

                GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled=false;

            } else {
                lossMusic.enabled = true;

                GameObject.Find("BurningPlayerSound").GetComponent<AudioSource>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;

                //SceneManager.LoadScene ("Level 4 (Lose)");
            }
            //}
        }
		
		if (Input.GetKeyDown(dropButton)){
			dropStones ();
		}
		if (Input.GetKeyDown(dropButton2)){
			dropStones ();
		}

        updateHealth();
/*
        stoneText.text = " " + minedStones;
		wallText.text = " " + (wallInventory.Count);
		TreeText.text = " " + choppedTrees;
		wallTextP2.text = " " + ( wallInventory2.Count);
		*/

		if (currentHealth <= 0) {
			gameOn = false;
			winner = false;
		}

//		if (burning) {
//			if (!smokeEffect.isPlaying) {
//				smokeEffect.Play ();
//			}
//		}
//		if (!burning) {
//			if (smokeEffect.isPlaying) {
//				smokeEffect.Stop ();
//			}
//		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("tutorial");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("main");
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SceneManager.LoadScene ("main 2");
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			SceneManager.LoadScene ("DemoTreesOnly");
		}


	}

    float calculateHealth()
    {
        return currentHealth / maxHealth;
    }

    void updateHealth()
    {
        healthBar.value = calculateHealth();
        if (burning)
        {
            currentHealth -= .01f;
        }
        if (burning && currentHealth <= 0)
		{	

			//Destroy(GameObject.Find("Player"));
			gameOn = false;
			winner = false;
        }
    }

	public void addBucket () {
		// 0.025f per bucket should fill the trench in 45 seconds, 0.02f in 1 minute, 0.007f in 2:20 minutes

		string sceneName = currentScene.name;


		if (sceneName == "tutorial") 
		{
			waterFillAmount += 0.0110f;
		}
		else 
		{
			// 0.025f per bucket should fill the trench in 45 seconds, 0.02f in 1 minute, 0.007f in 2:20 minutes
			waterFillAmount += 0.0055f;
		}

		waterLeft.fillAmount = waterFillAmount;
		waterRight.fillAmount = waterFillAmount;
		if (waterFillAmount >= 1.0f) {
			gameOn = false;
			winner = true;
		}
	}

    public void addPlayerBucket()
    {
        // 0.025f per bucket should fill the trench in 45 seconds, 0.02f in 1 minute, 0.007f in 2:20 minutes
        waterFillAmount += 0.0008f;
        waterLeft.fillAmount = waterFillAmount;
        waterRight.fillAmount = waterFillAmount;
        if (waterFillAmount >= 1.0f)
        {
            gameOn = false;
            winner = true;
        }
    }

    public LayerMask layerMask;

	public void generateStone() {
		//GameObject[] stonesArray;
		Vector3 fireCollider = firePos.transform.position; 

		print(fireCollider.x - 6.0f);
		//stonesArray = GameObject.FindGameObjectsWithTag("stone");
		//float dist = Vector3.Distance(spawnPoint, transform.position);
		Vector3 spawnPoint = new Vector3 ((Random.Range (-.18f, (fireCollider.x - 6.0f))), (Random.Range (-4.7f, 4.7f)), -0.04882813f);
		//int colliding = Physics2D.OverlapCircle (spawnPoint, 5.0f);//, LayerMask.NameToLayer("Stone"));
		//Collider2D [] colliders = Physics2D.OverlapCircleAll(spawnPoint, 3f,layerMask);
		//print (colliders.Length);
		while (Physics2D.OverlapCircle(spawnPoint, .3f, layerMask)){//colliders.Length > 0) 
			spawnPoint = new Vector3 ((Random.Range (-.18f, (fireCollider.x - 6.0f))), (Random.Range (-4.7f, 4.7f)), -0.04882813f);
			//colliders = Physics2D.OverlapCircleAll(spawnPoint, 3f,layerMask);
		}
		Instantiate(stones, spawnPoint, Quaternion.Euler(0.0f, 0.0f, 0.0f)); 

	}
	public void dropStones (){
		
		if (minedStones > 0){
			minedStones--;
		}

	}
	public void dropTrees (){//trees

		if (choppedTrees > 0){
			choppedTrees--;
		}

	}

}
