using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public float damage; // how much damage the enemy does
    public float damageRate; // how often the enemy can damage in seconds
    public float pushBackForce; // pushes back force from the damage 
   
    float nextDamage;
	
	void Start ()
    {
        nextDamage = 0f; // starts with 0, so the player can be damaged from beginning
	}
	
	
	void Update ()
    {
		        
	}

    void FixedUpdate()
    {
        
    }

    void onTriggerStay2D (Collider2D col)
    {
        if(col.tag == "Player" && nextDamage < Time.time) // if the collider touches anything named Player then ... 
        {
            PlayerHealth myPlayerHealth = col.gameObject.GetComponent<PlayerHealth>(); // gain reference to the playerHealth script 
            myPlayerHealth.addDamage(damage); // add damage to the player health which will remove health
            nextDamage = Time.time + damageRate; // making sure the damage rate is a constant

            ForcePush(col.transform); // give the player a force when hit
        }
    }

    void ForcePush(Transform forcedObject)
    {
        Vector2 pushDirection = new Vector2(0, (forcedObject.position.y - transform.position.y)).normalized; // puting force on the oposite direction of the enemy
        pushDirection *= pushBackForce; // adding movement to the object
        Rigidbody2D push = forcedObject.gameObject.GetComponent<Rigidbody2D>(); // adding reference of an object to a new rigid body 
        push.velocity = Vector2.zero; // giving the player no velocity or force in any direction
        push.AddForce(pushDirection, ForceMode2D.Impulse); // add the force to the player
    }
}
