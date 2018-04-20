using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerController : MonoBehaviour {

    //Problems found - 
    // if the archer isn't perfectly on the ground when the game is first started then he will assume he's walking and it will play the running animation even 
    // though he isn't moving.

   // I have tried to use box collider to detect walls and the player but it didn't work.
   // I have also tried to use the transformation position to detect walls and the player but that also gave me troubles when I wanted the archer to shoot at the enemy as
   // well as retreating from the player if the player gets close
   // I also attempted to use NavMesh but this was a bad idea as it didn't function at all.

   // Another problem I have experienced is that the archer detects the player and successfuly shoots but doesn't stop shooting when the player leaves archer's sight.
   
   // Flipping the archer, this was the single most time consuming issue. I was able to make this work quite easily with the player but not with the archer. The problem was mainly
   // bot transforming the localScale.

   // If the archer has not been placed on the ground perfectly then he will not successfuly walk around and detect using raycast for some reason. 




   // Successful ideas - 
   // I found out raycasting is really useful in both 2D and 3D through the unity website. I used their refference as a guideline to solving my problem.
   // 
   // Time between shots, this was a great idea that improved the AI so much more, in the beginning the archer constatly shot the player without any pause. After I implemented 
   // time between shots it change the archer and gave the chance for the player to fight back.



    private Transform player; 
   
    Rigidbody2D rb; 
    private Animator anim;

    public LayerMask ground;
    public Transform target;
    public LayerMask targetPlayer;

    public Transform originPoint; // point where the raycast starts
    public Transform originPoint2; // second origin point 
    public Transform OriginToDetctPlayer; // origin point to detect the player
    public Transform OriginToDetctPlayerLower; // origin point to detect the player
    public Transform OriginToDetctPlayerBehind; // origin point to detect the player from behind the enemy

    public float enemySpeed; // enemy's speed
    private Vector2 direction = new Vector2(1, 0); // direction which the ranger is facing, -1 would be left and 1 is right. It's used for flipping both the ranger and raycast
    private Vector2 directionBehind = new Vector2(-1, 0); 

    public float proximityRangeToWalls; // ranger detects walls
    public float proximityRangeToPlayer; // proximity range to detect the player

    bool facingRight = true; // this is to check whether the characer is facing left or right

    public Transform bowEnd; // this is the point which the arrow will come out of
    public GameObject Arrow; // this is the arrow prefab

    private float timeBetweenShots; 
    public float startTimeBetweenShots;
   
    public float retreatDistance;

    

    void Start()
    {
        anim = GetComponent<Animator>(); // retrieving the Animator component attached to the archer
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>(); // retrieving the rigidbody component from the archer
        timeBetweenShots = startTimeBetweenShots; // reseting time between shots
        
    }

    void Update()
    {
        

        
        // THIS IS JUST TO DRAW THE LINE !   !   !  !
        Debug.DrawRay(originPoint.position, direction * proximityRangeToWalls); // creating a raycast line point out of the ranger to detect objects
        Debug.DrawRay(originPoint2.position, direction * proximityRangeToWalls); // creating a raycast line to detect if the ground has come to an end
        Debug.DrawRay(OriginToDetctPlayer.position, direction * proximityRangeToPlayer); // this is to detect the upper part of the player
        Debug.DrawRay(OriginToDetctPlayerLower.position, direction * proximityRangeToPlayer); // and this is to detect the lower part of the player
        Debug.DrawRay(OriginToDetctPlayerBehind.position, directionBehind * proximityRangeToPlayer/2); // and this to detect the player if he is behind the enemy

        // This is to detect
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, proximityRangeToWalls, ground); // origin point is where the raycast starts, direction is which direction it's pointing to, and range is how long the raycast is.
        RaycastHit2D hit2 = Physics2D.Raycast(originPoint2.position, direction, proximityRangeToWalls, ground); // second origin point
        RaycastHit2D hitDetectPlayer = Physics2D.Raycast(OriginToDetctPlayer.position, direction, proximityRangeToPlayer, targetPlayer); // origin point to detect from upper enemy to player
        RaycastHit2D hitDetectPlayerLower = Physics2D.Raycast(OriginToDetctPlayerLower.position, direction, proximityRangeToPlayer, targetPlayer); // origin point to detect the lower part of the player
        RaycastHit2D hitDetectPlayerBehind = Physics2D.Raycast(OriginToDetctPlayerBehind.position, directionBehind, proximityRangeToPlayer/2, targetPlayer); // origin point to detect the lower part of the player

        // getting the ranger to compute the distance between himself and the player and checking if stopping distance is less than his position AND if hitDetectPlayer is true
        if (Vector2.Distance(transform.position, player.position) < proximityRangeToPlayer && hitDetectPlayer == true)
        {
            transform.position = this.transform.position; // if the above statement passes then stop moving the ranger
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance) // if the retreat distance is less than the distance between ranger and player then...
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime); 
        }


        if ( hit2 == true ) // if the first ogrigin point touches "Ground" layer then ... 
        {
            Flip(); // flip the ranger
            enemySpeed *= -1; // give ranger opposite speed
            direction *= -1; // opposite direction
            directionBehind *= -1;
        }
        if  (hit == false ) // if the second origin point STOPS toucing "Ground" layer then...
        {
            Flip(); // flip the ranger
            enemySpeed *= -1; // give ranger opposite speed
            direction *= -1; // opposite direction
            directionBehind *= -1;
        }
        if (hitDetectPlayer == true || hitDetectPlayerLower == true) // if the upper or lower raycast detets the player then...
        {
            anim.SetBool("isAttacking", true);
            shootArrow();
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
            enemySpeed = 0; // I added this to stop the enemy from moving when he was shooting but this made the enemy not move even when the player wasnt in range
                                 
        }
        if (hitDetectPlayerBehind == true)
        {
            Flip(); // flip the ranger  
            anim.SetBool("isAttacking", true);
            
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
            enemySpeed = 0; // I added this to stop the enemy from moving when he was shooting but this made the enemy not move even when the player wasnt in range
            directionBehind = -directionBehind; // opposite direction

             shootArrow();
            //enemySpeed *= 1; // give ranger opposite speed


        }
        else if (hitDetectPlayer == false || hitDetectPlayerLower == false)
        {
            anim.SetBool("isAttacking", false);
            

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
        

        anim.SetFloat("Speed", Mathf.Abs(enemySpeed));

        rb.velocity = new Vector2(enemySpeed, rb.velocity.y); // giving speed to the ranger on x-axis.

        
    }

    void shootArrow() // getting the arrow to shoot
    {

        if (timeBetweenShots <= 0 && facingRight) // if the enemy hasn't shot yet and is facing to the right (1,0)
        {

            Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 0))); // draw the arrow  
            timeBetweenShots = startTimeBetweenShots; 

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (timeBetweenShots <= 0 && !facingRight) // if the enemy hasn't shot yet and is facing to the right (-1,0)
        {

            Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 180))); // draw the arrow with 180 rotation
            timeBetweenShots = startTimeBetweenShots;

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        //if (timeBetweenShots <= 0)
        //    if (facingRight)
        //    {
                
        //        Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        //    }
        //    else if (!facingRight)
        //    {
                
        //       Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        //    }
    }
           
    }
   

