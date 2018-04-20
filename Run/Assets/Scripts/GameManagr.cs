using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagr : MonoBehaviour {

    [SerializeField] 
    GameObject[] switches; // array to store all the switches in the inspector

    [SerializeField]
    GameObject exitDoor; // the exit door prefab where the player comes out of 

    [SerializeField]
    GameObject entryDoor; // the entry door prefab where the player enters, this is instantiated when all the switches are turned on

    int noOfSwitches = 0;

    [SerializeField]
    Text switchCount; // this is just the text to show many switches left, this is then used in the UI 



    public GameObject portal; // portal wich is instantiated when all the switches have been turned off
   

   
	void Start ()
    {
        getNoOfSwitches(); // a public function that that starts at the beginning of the game
                        
	}
	
	
	void Update ()
    {

        getExitDoorState();
        switchCount.text = getNoOfSwitches().ToString();
	}

    public int getNoOfSwitches()
    {

        int x = 0;

        for (int i = 0; i < switches.Length; i++) // iterating through the switches to ...
        {
            if(switches[i].GetComponent<Switch>().isOn == false) // find out if any of them are false
            
                x += 1; // and move on to the next switch
            
            else if (switches[i].GetComponent<Switch>().isOn == true) // other wise check which switch is turned on
            
                noOfSwitches -= 1; 
            
        }
        noOfSwitches = x;

       return noOfSwitches;
    }

    public void getExitDoorState()
    {
        if (noOfSwitches <= 0) // if the number of switches that are false is less or equal to 0 then ... 
        {
            exitDoor.GetComponent<Door>().openDoor(); // make the exit door to open 
            myPortal(); 
            
            
        }
    }

    public void myPortal()
    {
        
        Instantiate(portal, exitDoor.transform.position, exitDoor.transform.rotation); // spawning the portal at the entry door. 
                
    }

    
}
