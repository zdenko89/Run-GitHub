using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerController : MonoBehaviour {



    

    private Transform player; 
    PlayerMove playerScript;
    Rigidbody2D rb;
    private Animator anim;

    public LayerMask ground;
    public Transform target;
    public LayerMask targetPlayer;

    public Transform originPoint; // point where the raycast starts
    public Transform originPoint2; // second origin point 
    public Transform OriginToDetctPlayer; // origin point to detect the player

    public float enemySpeed; // enemy's speed
    private Vector2 direction = new Vector2(1, 0); // direction which the ranger is facing, -1 would be left and 1 is right

    public float proximityRangeToWalls; // ranger detects walls
    public float proximityRangeToPlayer; // proximity range to detect the player

    bool facingRight = true; // this is to check whether the characer is facing left or right

    

    public Transform bowEnd;
    public GameObject Arrow;

    private float timeBetweenShots;
    public float startTimeBetweenShots;
   
    public float retreatDistance;
    

    void Start()
    {
        anim = GetComponent<Animator>(); // retrieving the Animator component attached to the archer
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        

        rb = GetComponent<Rigidbody2D>(); // retrieving the rigidbody component from the archer

        Debug.DrawRay(originPoint.position, direction * proximityRangeToWalls); // creating a raycast line point out of the ranger
        Debug.DrawRay(originPoint2.position, direction * proximityRangeToWalls);
        Debug.DrawRay(OriginToDetctPlayer.position, direction * proximityRangeToPlayer);

        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, proximityRangeToWalls, ground); // origin point is where the raycast starts, direction is which direction it's pointing to, and range is how long the raycast is.
        RaycastHit2D hit2 = Physics2D.Raycast(originPoint2.position, direction, proximityRangeToWalls, ground); // second origin point
        RaycastHit2D hitDetectPlayer = Physics2D.Raycast(OriginToDetctPlayer.position, direction, proximityRangeToPlayer, targetPlayer);

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
        }
        if  (hit == false ) // if the second origin point STOPS toucing "Ground" layer then...
        {
            Flip(); // flip the ranger
            enemySpeed *= -1; // give ranger opposite speed
            direction *= -1; // opposite direction
        }
        if (hitDetectPlayer == true ) // if the second origin point STOPS toucing "Ground" layer then...
        {
            shootArrow();
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
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

        if (timeBetweenShots <= 0)
        {

            Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            timeBetweenShots = startTimeBetweenShots;

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (timeBetweenShots <= 0)
            if (facingRight)
            {
                Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            }
            else if (!facingRight)
            {
                Instantiate(Arrow, bowEnd.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
    }
           
    }
   

