using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

    // I made this script to work with many different objects but I didn't make it work that way.
    // This script is attached to the barrels.

    public GameObject destroyEffect; // this is the effect when the barrel is destoryed, ref to prefab of the particle system
    ProjectileController hitDetector; // this is a ref to the script which is attached to the projectile that the player shoots
    Animator anim; // ref to the animator on the object


    void Start ()
    {
        hitDetector = GetComponentInParent<ProjectileController>(); // retrieving the component attached to the projectile, which is shot by the player
        anim = GetComponent<Animator>();
        anim.SetBool("exploded", false); // making this animation false initially. 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile") // if the projectile hits the barrel then ... 
        {
            Instantiate(destroyEffect, transform.position, transform.rotation); // expose the particle system and ... 
            Destroy(gameObject); // destroy the barrel
            anim.SetBool("exploded", true); // and play the animation

        }
    }
}
