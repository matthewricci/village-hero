using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spillOil : MonoBehaviour {

	public float size;
	public float timer;

	// Use this for initialization
	void Start () {
		size = Random.Range (1.3f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (timer < 1.5f) {
			timer += Time.deltaTime;

			float newScale = Mathf.Lerp(1.0f, size, timer / 1.5f);
			transform.localScale = new Vector3 (
				newScale,
				newScale,
				1.0f
			);
		}
	}
}
