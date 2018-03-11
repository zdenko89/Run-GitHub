using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerController : MonoBehaviour {

    
  
    public float retreadDistance;  // distance between ranger and player, if too close then the ranger will move back

   

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject Arrow;

    private Transform player;

    PlayerMove playerScript;
    
    Rigidbody2D rb;
    float myWidth;

    public LayerMask ground;

    public Transform target;
    
   
    public Transform originPoint; // point where the raycast starts
    public float enemySpeed; // enemy's speed
    private Vector2 direction = new Vector2(1, 0); // direction which the ranger is facing, -1 would be left and 1 is right
    public float proximityRange; // ranger detects where the player is depending on this distance
    bool facingRight = true; // this is to check whether the characer is facing left or right

    void Start ()
    {
        
        
	}

    void Update()
    { 


        rb = GetComponent<Rigidbody2D>();
        Debug.DrawRay(originPoint.position, direction * proximityRange);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, proximityRange, ground); // origin point is where the raycast starts, direction is which direction it's pointing to, and range is how long the raycast is.

        if (hit == true)
        {
            Flip();
            enemySpeed *= -1;
            direction *= -1;


        }

        


    }

    public void Flip() // method to make the character face the correct direction by flipping the scene and not the character]
                       // this saves time making individual animations
    {
        facingRight = !facingRight; // face the opposite direction
        Vector3 theScale = transform.localScale; // retrieve the local scale
        theScale.x *= -1; //flip x-axis
        transform.localScale = theScale; // apply back to local scale
    }

    void FixedUpdate ()
    {

        rb.velocity = new Vector2(enemySpeed, rb.velocity.y); // giving speed to the ranger on x-axis.



        if (Vector2.Distance(transform.position, player.position) > proximityRange) // if the distance between the ranger and player is greater than stopping distance...
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime); // move the ranger towards the player

        }
        else if (Vector2.Distance(transform.position, player.position) > proximityRange && Vector2.Distance(transform.position, player.position) > retreadDistance) // check to see if the player is near enough... 
        {
            transform.position = this.transform.position; // stop moving
        }
        else if (Vector2.Distance(transform.position, player.position) < retreadDistance) // if the distance between ranger and player is less than retreatDistance then ... 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime); // make the player move back
        }


        if (timeBetweenShots <= 0)
        {
            Instantiate(Arrow, transform.position, Quaternion.identity); // spawn the projectile prefab at ranger's position
            timeBetweenShots = startTimeBetweenShots; // this is to restrict the ranger from constantly shooting
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }



    }

   
}
