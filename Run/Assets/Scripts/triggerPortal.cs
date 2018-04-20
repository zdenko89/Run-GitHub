using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPortal : MonoBehaviour {

    PlayerController myPlayer;
    public GameObject Exit;
   

 
    
    void Start ()
    {
        myPlayer = GetComponent<PlayerController>();
       
        
    }
	
	
	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            
            myPlayer.transform.position = Exit.transform.position;
            
        }
    }
}
