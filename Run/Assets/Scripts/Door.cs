using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{


    Animator anim; // getting the animator

    [SerializeField]
    GameObject doorType; // getting game object for door type

    int stateOfDoor = 1; // this is to track the state of the door

    void Start()
    {
        // initialized the animator component
        anim = GetComponent<Animator>();

        if (doorType.name == "EntryDoor") // setting entry door to open
        {
            anim.SetFloat("DoorState", 3);
        }

        if (doorType.name == "ExitDoor") // setting exit door to locked
        {
            lockedDoor();
        }
    }


    public void lockedDoor() // function to lock the door and set it's state
    {

        if (doorType.name == "ExitDoor") 
        {
            anim.SetFloat("DoorState", 1);
            stateOfDoor = 1;
        }

    }

    public void unlockDoor() // function to unloced the door and set it's state
    {

        if (doorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 2);
            stateOfDoor = 2;
        }

    }

    public void openDoor() // function to open the door and set it's state
    {

        if (doorType.name == "ExitDoor")  
        {
            anim.SetFloat("DoorState", 3);
            stateOfDoor = 3;
        }

    }

    public void doorState(int state) // function to set the state of the door
    {
        if (state == 1 && doorType.name == "ExitDoor")
        {
            lockedDoor();
        }
        if (state == 2 && doorType.name == "ExitDoor")
        {
            unlockDoor();
        }
        if (state == 3 && doorType.name == "ExitDoor")
        {
            openDoor();
        }
    }

    public int getDoorState()
    {
        return stateOfDoor;
    }
}