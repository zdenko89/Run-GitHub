using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

    public GameObject destroyEffect;
    ProjectileController hitDetector;
    Animator anim;


    void Start ()
    {
        hitDetector = GetComponentInParent<ProjectileController>();
        anim = GetComponent<Animator>();
        anim.SetBool("exploded", false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            anim.SetBool("exploded", true);

        }
    }
}
