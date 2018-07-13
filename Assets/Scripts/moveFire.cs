using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFire : MonoBehaviour {

	public villageManager manager;
	public FireFlicker flickAnim;

	public float fireSpeed;
	public bool stopped;
	public bool startedEndAnim;

	// Use this for initialization
	void Start () {
		//fireSpeed = 0.13f;  // one-minute game
		//fireSpeed = 0.05f;  // 2 min 20 sec for fire to traverse
		//fireSpeed = 0.040f; //3 minutes to lose
		fireSpeed = 0.045f;
		stopped = false;
		startedEndAnim = false;
		flickAnim = GetComponentInChildren<FireFlicker> ();
	}
	
	// Update is called once per frame
	void Update () {
		// 0.16f should take 35 seconds to get to the bridge
		if (!(stopped)) {
			transform.position = new Vector3 (transform.position.x - fireSpeed * Time.deltaTime, transform.position.y, transform.position.z);
		}

		if (!(stopped) && !(manager.gameOn)) {
			if (!startedEndAnim) {
				startedEndAnim = true;
				fireSpeed = 3.0f;
				Destroy(GetComponent<Collider2D> () );
				Destroy (GetComponent<Rigidbody2D> () );
			}
		}
//		if (!(manager.gameOn)) {
//			stopped = true;
//		}

//		if (!(manager.gameOn)) {
//			fireSpeed = 3.0f;
//		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "stone") {

			print ("stone");
			fireSpeed = 0.0f;
			stopped = false;
			flickAnim.enabled = false;
			flickAnim.fire_renderer.color = new Color (
				0.647f,
				0.376f,
				0.262f
			);
			moveStone stone = coll.gameObject.GetComponent<moveStone> ();
			stone.StartCoroutine ("HeatStoneAnimation");
			stone.moveable = false;
		}


		if (coll.gameObject.tag == "woodenWall") {
			print ("Wall");
			fireSpeed = 0.0f;
			stopped = false;
			flickAnim.enabled = false;
			flickAnim.fire_renderer.color = new Color (
				0.647f,
				0.376f,
				0.262f
			);
			coll.gameObject.GetComponent<placeWood> ().touchingFireBars.Add (this);
//			slowFire wall = coll.gameObject.GetComponent<slowFire> ();
//			wall.onFire = true;
		}
	}
}
