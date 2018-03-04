using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

    public float enemySpeed;

    Animator enemyAnim;
    public GameObject enemyArcher;
    bool flip = true; // checks to see if it needs to flip
    bool facingRight = true; // boolean to check if the archer is facing right
    float flipTime = 5f;
    float nextFlipChance = 0f;

    public float AttackTime; // how long before the archer attacks after the player has entered the collision zone
    float StartAttackTime;
    bool Attacking;
    Rigidbody2D enemyRigid;

	void Start ()
    {
        enemyAnim = GetComponentInChildren<Animator>(); // retrieving the reference to the animator of Archer, had to access with 'Children' because it's a child of this script
        enemyRigid = GetComponent<Rigidbody2D>(); // retrieving reference to the rigidbody of the object which this script is attached to 
        
    }
	
	
	void Update ()
    {
		// this set of code is to make the archer flip left and right
        if(Time.time > nextFlipChance)  // if the current game time is greater than nextFlipChance then...
        {
            if(Random.Range (0,10) >= 5) // if the random given is between 0 and 10 is greater than 5 then ...  
            {
                flipFacing(); // instantiate this method. (50% chance it will flip)
            }
            nextFlipChance = Time.time + flipTime; 
        }

	}

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {
            if (facingRight && collider.transform.position.x < transform.position.x)
            {
                flipFacing();

            }
            else if (!facingRight && collider.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            flip = false;
            Attacking = true;
            StartAttackTime = Time.time + AttackTime;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player") // if the collision is with a collider tagged "Player" then ... 
        {
            if ( StartAttackTime < Time.time ) // if start attack time is more than the current game play time then ... 
            {
                if ( !facingRight ) // if the enemy is not facing right then ... 
                {
                    enemyRigid.AddForce(new Vector2(-1, 0)* enemySpeed); // add force on the rgidbody (archer) to the negative x direction
                } else enemyRigid.AddForce(new Vector2(1, 0) * enemySpeed); // othjer wise add speed on the positive x direction
                enemyAnim.SetBool("isAttacking", Attacking); // enabling the attacking animation
            }
        }
    }

     void OntriggerExit2D (Collider2D collider)
     {
        if(collider.tag == "Player")
        {
            flip = true;
            Attacking = true;
            enemyRigid.velocity = new Vector2(0f, 0f);
            enemyAnim.SetBool("isAttacking", Attacking);
        }
        
     }

    void flipFacing()
    {

        if (!flip) return; // if the archer can't flip then just return
        float facingX = enemyArcher.transform.localScale.x; // seeing which direction the archer is facing
        facingX *= -1f; // and flip it the oppsite direction
        enemyArcher.transform.localScale = new Vector3(facingX, enemyArcher.transform.localScale.y, enemyArcher.transform.localScale.z); // completing the transformation of the archer to face the opposite direction
        facingRight = !facingRight;
    }
}
