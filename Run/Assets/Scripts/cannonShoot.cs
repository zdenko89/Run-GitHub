using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShoot : MonoBehaviour {

    public GameObject cannonBall; // the prefab ref, which is shot out of the base cannon
    public float shootTime; 
    public int chanceShoot;
    public Transform shootFrom;

    float timeBetweenShots; // time between shots, this is to make sure that the cannon will not be shooting constantly or too not enough
    Animator cannonAnim; 

	void Start ()
    {

        cannonAnim = GetComponentInChildren<Animator>();
        timeBetweenShots = 0f;

	}
	
	
	void Update ()
    {
		


	}

    void OnTriggerStay2D(Collider2D collision) 
    {
        
        if(collision.tag == "Player" && timeBetweenShots < Time.time) // if the player enters the collision/trigger zone and time 
                                                                     // between shots is less active scene time then ... 
        {
            timeBetweenShots = Time.time + shootTime; // time between shots is equal to active scene time and shoot time
            

            if (Random.Range(0, 10) >= chanceShoot) // if the random number is more or equal than chance shoot then  ... 
            {
                Instantiate(cannonBall, shootFrom.position, Quaternion.identity); // Quaternion.identity = no rotation shoot the cannon balls
                cannonAnim.SetTrigger("cannonShoot"); // and play the animation
            }
        }


    }
}
