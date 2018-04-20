using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrap : MonoBehaviour
{

    PlayerController myPlayer;

    public Transform[] maces;

    public GameObject teleportTo;


    
    




    void Start()
    {
        myPlayer = GetComponent<PlayerController>();
        

    }

 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("works");

            for (int i = 0; i <= maces.Length; i++)
            {
               // Instantiate(maces[i], teleportTo.transform.position, teleportTo.transform.rotation);
            }
                          

            

        }
    }
}