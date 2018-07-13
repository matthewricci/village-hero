using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carryWater : MonoBehaviour {

	public villageManager manager;

	public Color full;
	public Color empty;

	public bool hasWater;
	public float walkingSpeed;

	public Collider2D villager_coll;
	public Rigidbody2D villager_rb;
	public SpriteRenderer villager_renderer;

    private AudioSource fillingTrenchSound;

    // Use this for initialization
    void Start () {
		hasWater = false;
		walkingSpeed = 2.0f;
        fillingTrenchSound = GameObject.Find("FillingTrenchSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (manager.gameOn) {
		
			if (hasWater) {
				transform.position = new Vector3 (
					transform.position.x + Time.deltaTime * walkingSpeed,
					transform.position.y,
					transform.position.z
				);
			} else {
				transform.position = new Vector3 (
					transform.position.x - Time.deltaTime * walkingSpeed,
					transform.position.y,
					transform.position.z
				);
			}
		
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (hasWater) {
			if (other.tag == "trench") {
				villager_renderer.color = empty;
				hasWater = false;
				manager.addBucket ();
                fillingTrenchSound.Play();
            }
		} else {
			if (other.tag == "water") {
				villager_renderer.color = full;
				hasWater = true;
			}
		}
	}
}
