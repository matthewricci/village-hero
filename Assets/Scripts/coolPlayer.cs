using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolPlayer : MonoBehaviour
{

    public villageManager manager;

    private AudioSource coolPlayerSound;
    private AudioSource burningPlayerSound;

    // Use this for initialization
    void Start()
    {
        coolPlayerSound = GameObject.Find("CoolPlayerSound").GetComponent<AudioSource>();
        burningPlayerSound = GameObject.Find("BurningPlayerSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        print("Collided! coll: " + coll.gameObject.name);
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2")
        {
            if (manager.burning)
            {        
                coolPlayerSound.Play(); 
            }
            manager.burning = false;
            burningPlayerSound.Stop();
        }
    }
}
