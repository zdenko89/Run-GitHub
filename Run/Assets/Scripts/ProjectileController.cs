using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    Rigidbody2D projectile;
    public float projectileSpeed;

    void Start()
    {

    }

    void Awake()
    {
        shoot();
    }

    void Update()
    {

    }

    public void stopProjectile()
    {
        projectile.velocity = new Vector2(0,0);
    }
        
    public void shoot()
    {

        projectile = GetComponent<Rigidbody2D>();

        if (transform.localRotation.z > 0) // 
        {
            projectile.AddForce(new Vector2(-1, 0) * projectileSpeed, ForceMode2D.Impulse);
        }
        else
        {
            projectile.AddForce(new Vector2(1, 0) * projectileSpeed, ForceMode2D.Impulse);
        }

    }
}
