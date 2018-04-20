using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    // Impulse : Add an instant force impulse to the rigidbody, using its mass.
    // ForceMode2D : AddForce function impacts how your GameObject moves by allowing you to define your own force vector, as well as choosing how to apply this force to the GameObject
    // localRotation : 

    // this script a projectile controller which is shot by the enemy rangers

    Rigidbody2D Arrow; // prefab ref to the arrow being shot
    public float ArrowSpeed; // speed at which the arrow will be shot at

  
    void Awake()
    {
        shoot();
    }

    void Update()
    {

    }

    public void stopArrow()
    {
        Arrow.velocity = new Vector2(0, 0); // this stops the arrow's movement
    }

    public void shoot()
    {

        Arrow = GetComponent<Rigidbody2D>(); // retrieving the rigidbody component attached to the prefab

        if (transform.localRotation.z > 0) 
        {
            Arrow.AddForce(new Vector2(-1, 0) * ArrowSpeed, ForceMode2D.Impulse); // instantly add the vector information to the arrow to the left
        }
        else
        {
            Arrow.AddForce(new Vector2(1, 0) * ArrowSpeed, ForceMode2D.Impulse); // instantly add the vector information to the arrow to the left
        }

    }
}
