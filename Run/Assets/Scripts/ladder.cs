using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour {

    // Ladder controller script

    public float speedUp; // player's speed moving up or down
    public bool onLadder; 

    void Start ()
    {
        
	}
	
	
	void Update ()
    {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && Input.GetKey(KeyCode.W)) // if the player collides with the ladder's collider and w is pressed then...
        {
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedUp); // move the player up on the Y-axis with the chosen speed in the inspector
            
            
        }
        else if (col.gameObject.tag == "Player" && Input.GetKey(KeyCode.S)) // if the player collides with the ladder's collider and s is pressed then...
        {
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speedUp);  // move the player down on the Y-axis with the chosen speed in the inspector

        }
        else
        {
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1); 
            
        }
    }
}
