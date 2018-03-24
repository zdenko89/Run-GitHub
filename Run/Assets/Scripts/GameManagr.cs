using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagr : MonoBehaviour {

    [SerializeField]
    GameObject[] switches;

    [SerializeField]
    GameObject exitDoor;

    [SerializeField]
    GameObject entryDoor;

    int noOfSwitches = 0;

    [SerializeField]
    Text switchCount;



    public GameObject portal;
   

   
	void Start ()
    {
        getNoOfSwitches();
                        
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
        
        Instantiate(portal, exitDoor.transform.position, exitDoor.transform.rotation); 
                
    }

    
}
