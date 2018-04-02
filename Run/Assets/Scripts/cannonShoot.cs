using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShoot : MonoBehaviour {

    public GameObject cannonBall;
    public float shootTime;
    public int chanceShoot;
    public Transform shootFrom;

    float timeBetweenShots;
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
        
        if(collision.tag == "Player" && timeBetweenShots < Time.time)
        {
            timeBetweenShots = Time.time + shootTime;
            

            if (Random.Range(0, 10) >= chanceShoot)
            {
                Instantiate(cannonBall, shootFrom.position, Quaternion.identity); // Quaternion.identity = no rotation
                cannonAnim.SetTrigger("cannonShoot");
            }
        }


    }
}
