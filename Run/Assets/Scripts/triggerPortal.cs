using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPortal : MonoBehaviour {

    PlayerController myPlayer;
    public GameObject enterDoor;
   

 
    
    void Start ()
    {
        myPlayer = GetComponent<PlayerController>();
       
        
    }
	
	
	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")   // && Input.GetKeyDown(KeyCode.E)
        {
            print("works");
            myPlayer.transform.position = enterDoor.transform.position;
            
        }
    }
}
