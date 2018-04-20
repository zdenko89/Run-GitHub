using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour {


    public int StartamountOfLives; // the amount of starting lives the player gets
    public int lifeCounter; // counter to check how many lives the player has
    private Text text; // text ref to the text in the canvas
	
	void Start ()
    {
        text = GetComponent<Text>(); // retrieving the component to edit the amount of lives lives left 

        lifeCounter = StartamountOfLives; // letting the life counter know how many lives left at the start of the game
	}
	
	
	void Update ()
    {
        text.text =  "Lives:  " + lifeCounter; 
	}

    

    public void RemoveLife()
    {
        lifeCounter--;
    }
}
