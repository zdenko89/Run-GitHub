using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrel : MonoBehaviour {

    public GameObject destroyEffect;
    ProjectileController hitDetector;

    void Start ()
    {
        hitDetector = GetComponentInParent<ProjectileController>();
    }


    private void OnTriggerEnter2D(Collider2D collisionBarrel)
    {
        if (collisionBarrel.tag == "projectile")
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
