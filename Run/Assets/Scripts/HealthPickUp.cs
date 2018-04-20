using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    // script for the player to pick up health and add it to his current health
	
    public float healthAmount;

	void Start ()
    {
		

	}
	
	
	void Update ()
    {
		

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") // if the heart collides with the player then ... 
        {
            PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>(); // retrieving player's health script
            health.addHealth(healthAmount); // adding health to player
            Destroy(gameObject); // destroy the heart
            
        }
    }
}
