using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingPickaxe : MonoBehaviour {

	public float pickaxeSpeed;

	// Use this for initialization
	void Start () {
		pickaxeSpeed = 500.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward, -pickaxeSpeed * Time.deltaTime);
	}
}
