using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    [SerializeField]
    GameObject switchOn;

    [SerializeField]
    GameObject switchOff;

    public bool isOn = false;


   

	void Start ()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite; // automatically switch the sprite to off 
        
       
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite; // set the switch to on sprite
              
        isOn = true; // when triggered on collision it will set it to true
    }

    void OnTriggerStay(Collider other)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite; // set the switch to on sprite

        isOn = true; // when triggered on collision it will set it to true
    }
}
