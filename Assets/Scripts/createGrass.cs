using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createGrass : MonoBehaviour {

    public GameObject prefabGrass;
    public GameObject prefabGrassWithSpots;
    public GameObject prefabGrassWithFlowers;
    public GameObject prefabWater;

    // Use this for initialization
    void Start () {
        //-4.8,4.8
        //2.509591
        GameObject water = GameObject.Find("Water");
        Vector3 spawnPos = new Vector3(-6.7f, 5.8f, 5.0f);
        int spaceBetweenSpotsOrFlowers = 7;
        for (int i = 0; i < 22; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                /*if (j == 0)
                {
                    Instantiate(prefabWater, spawnPos, Quaternion.identity).transform.parent = water.transform;
                }
                else
                {*/
                    if (spaceBetweenSpotsOrFlowers == 0)
                    {
                        if (Random.Range(0, 2) == 1) // 50/50 chance of spawning either flowers or spots because i cant decide when i want what
                        {
                            Instantiate(prefabGrassWithSpots, spawnPos, Quaternion.identity).transform.parent = transform;
                        }
                        else
                        {
                            Instantiate(prefabGrassWithFlowers, spawnPos, Quaternion.identity).transform.parent = transform;
                        }
                        spaceBetweenSpotsOrFlowers = 4;
                    }
                    else
                    {
                        Instantiate(prefabGrass, spawnPos, Quaternion.identity).transform.parent = transform;
                    }
                //}
                

                spawnPos = new Vector3(spawnPos.x + 0.53f, spawnPos.y, 5.0f);
                spaceBetweenSpotsOrFlowers--;
            }
            spawnPos = new Vector3(spawnPos.x - 13.78f, spawnPos.y - 0.53f, 5.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
