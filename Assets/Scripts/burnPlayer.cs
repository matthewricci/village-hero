using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class burnPlayer : MonoBehaviour {

	public villageManager manager;

	public Canvas healthCanvas; // used to toggle healthbar on and off
	public Image healthbar;		// used to represent health left for player
	public ParticleSystem smoke;	// Particle System, use setActive method to begin/stop smoking

    private AudioSource burnPlayerSound;
    private AudioSource burningPlayerSound;
    private AudioSource coolPlayerSound;

	// Use this for initialization
	void Start () {
		manager.burning = false;
		healthbar.fillAmount = 1.0f;
		healthCanvas.enabled = false;
        burnPlayerSound = GameObject.Find("BurnPlayerSound").GetComponent<AudioSource>();
        coolPlayerSound = GameObject.Find("CoolPlayerSound").GetComponent<AudioSource>();
        burningPlayerSound = GameObject.Find("BurningPlayerSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (manager.gameOn){
			if (manager.burning) {
				healthbar.fillAmount -= 0.06f * Time.deltaTime; // ~12.5 seconds until death
				if (healthbar.fillAmount <= 0.0f) {
					manager.numPlayersAlive--;
					if (manager.numPlayersAlive <= 0){
						manager.gameOn = false;
						manager.winner = false;	// already false, but just in-case
					}
					Destroy (gameObject);	// if burned alive, destroy this player
				}
			}
		}
	}

	IEnumerator restoreHealth() {
		while (!(manager.burning) && healthbar.fillAmount < 1.0f) {
			healthbar.fillAmount += 0.35f * Time.deltaTime;
			yield return null;
		}
		healthbar.fillAmount = 1.0f; 	// setting this at the end of a coroutine makes sure the fillAmount stays at a perfect 1.0f with no spillover
		healthCanvas.enabled = false;
	}

	void OnCollisionEnter2D( Collision2D coll) {
		print ("has wood: " + manager.hasWood);
		if (coll.gameObject.tag == "fire" && !manager.hasWood) {
			manager.burning = true;
            burnPlayerSound.Play();
            burningPlayerSound.Play();
            healthCanvas.enabled = true;
			if (!smoke.isPlaying) {
				smoke.Play ();
			}
		} else if (coll.gameObject.tag == "water"  && manager.gameOn) {
            if (manager.burning)
            {
                coolPlayerSound.Play();
            }
            manager.burning = false;
            burningPlayerSound.Stop();
            if (smoke.isPlaying) {
				smoke.Stop ();
			}
			//StartCoroutine ("restoreHealth");
		}
		else if (coll.gameObject.tag == "waterTrench"  && manager.gameOn) {
            if (manager.burning)
            {
                coolPlayerSound.Play();
            }
            manager.burning = false;
            burningPlayerSound.Stop();
            if (smoke.isPlaying) {
				smoke.Stop ();
			}
			//StartCoroutine ("restoreHealth");
		}
	}
}
