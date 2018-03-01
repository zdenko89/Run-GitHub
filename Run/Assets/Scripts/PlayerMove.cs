using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float maxSpeed = 10f; // movement speed of character
    bool facingRight = true; // this is to check whether the characer is facing left or right

    Animator anim; // refference to animator, which allows the character to interact with the animations

    private bool attack;

    private bool sliding = false;
    float slideTimer = 0f;
    public float maxSlideTime = 1.5f;
    //Reference to health collider game object 
    [SerializeField]
    GameObject healthCollider;

    bool grounded = false; // checks to see if the character is on ground
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround; // determine what ground is


    bool doubleJump = false;
    public float jumpForce = 700f;

    public Transform gunEnd;
    public GameObject projectile;

    private float rateofFire = 0f;
    private float nextFire = 0f;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() // happens every frame
    {
        HandleInput(); // calling this function.

        if ((grounded || !doubleJump ) && Input.GetKeyDown(KeyCode.Space)) // if the character is on the ground or not double jumping and pressing spacebar then ...
                                                                           
        {
            anim.SetBool("Ground", false); 
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce)); // allows the character to have jump, y-axis speed


            // making sure that pressing spacebar after double jumping is not going to allow another jump
            if ( !doubleJump && !grounded ) // if not double jumping and not on the ground then ... 
            {
                doubleJump = true; // double jump is true meaning it has been used, it would need to ground again for it to be false
            }
        }

    }

    void FixedUpdate () // called every physics step, not every frame like update()
        {

        HandleAttacks(); // calling this function
        ResetValues(); // calling this function

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float move = Input.GetAxis("Horizontal"); // moving the character based on key pressed, left or right in this case

        anim.SetBool("Ground", grounded); 

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y); // extract vertical speed information

        anim.SetFloat("Speed", Mathf.Abs(move));


        if ( grounded ) // is the player touching the ground?
        {

         doubleJump = false; // reseting  the values so that the player can't constantly jump

        }

        if (move > 0 && !facingRight)
        { // if the character is moving to the right then make it face not right, but left then ... 

            Flip(); // flip the scene

        }

        else if  (move < 0 && facingRight){ // else  if the character is moving left and facing right then...

            Flip(); // flip the scene

        }

        // extract the animation information from the animator, base 0 & tag "attack". If it's not attack then ...
        if (!this.anim.GetBool("IsSliding") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); // give the character speed
        }

        //if (sliding && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Slide")) // if slide (bool) is true and the animator "slide" is not triggered then set
        //{
        //    anim.SetBool("IsSliding", true); // this bool to true
        //}
        //else if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Slide")) // else if the animator "slide" is not triggered then ...
        //{
        //    anim.SetBool("IsSliding", false); // set the "slide" animator to false.
        //}


        if(Input.GetButtonDown("Slide") && !sliding) // if the correct key or button is pressed which is attached to "slide" and is currently not sliding then ... 
        {
            slideTimer = 0f; // how long does the character slide for
            anim.SetBool("IsSliding", true); // setting the bool attached to the sliding animator to true  
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // disables the box collider while sliding under an object 
            healthCollider.GetComponent<BoxCollider2D>().enabled = false; // disables the trigger on the box collider when sliding to avoid damage
            sliding = true; // sets sliding to true
        }
        if (sliding) // if sliding is set to true then ... 
        {
            slideTimer += Time.deltaTime; // increase the slide time
            
            if ( slideTimer > maxSlideTime ) // and if slide timer is greater than max slide time set then ... 
            {
                sliding = false; // set to false
                anim.SetBool("IsSliding", false); // stops the animation
                gameObject.GetComponent<BoxCollider2D>().enabled = true; // enables the box collider on the object (player)
                healthCollider.GetComponent<BoxCollider2D>().enabled = true; // and enables the health collider on the object (player)
            }
        }

    }

    void Flip() // method to make the character face the correct direction by flipping the scene and not the character]
                // this saves time making individual animations
    {
        facingRight = !facingRight; // face the opposite direction
        Vector3 theScale = transform.localScale; // retrieve the local scale
        theScale.x *= -1; //flip x-axis
        transform.localScale = theScale; // apply back to local scale
    }

    // this function will be in controll of all the attacks
    private void HandleAttacks()
    {
        // added the && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack") to this if statement to stop "attack" animation from being used while already one is triggered
        if (attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack")) // if attack is true and "attack" is not currently true then ...
        {
            anim.SetTrigger("attack"); // trigger this animation
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // it's supposed to stop the player from sliding when attack is triggered. But I think that velocity 
        }                                                        // would also need to be changed in fixedUpdate() for this to work. 
    }

    // this function will be in control of the input 
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // if the left shift key is pressed then ...
        {
            attack = true; // set attack to true

            
                fireProjectile();
            
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift)) // if the left shift key is pressed then ...
        //{
        //    sliding = true; // set slide to true
        //}
    }

    // this function will be in control of resteting the values from inputs (so they dont loop continuously
    private void ResetValues()
    {
        attack = false; // reseting the value for attack, so that it can be used again 
        //sliding = false; // reseting the values for slide
    }

    private void OnCollisionEnter2D(Collision2D collisionPlatformMoving)
    {
        if(collisionPlatformMoving.transform.tag == "MovingPlatform")
        {
            transform.parent = collisionPlatformMoving.transform;
        }
    }

    void fireProjectile()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateofFire;
        }
        if (facingRight)
        {
            Instantiate(projectile, gunEnd.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else if (!facingRight)
        {
            Instantiate(projectile, gunEnd.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        }
    }

}
