using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour {

    public float HitDamage;
    ProjectileController hitDetector; // reference to script
    public GameObject HitEffect;


    void Start ()
    {
		
	}

    void Awake()
    {
        hitDetector = GetComponentInParent<ProjectileController>();
    }

    void Update ()
    {
		
	}

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Explodable")) // if the game object collides with anything in the LayerMask of Explodable then...
        {
            hitDetector.stopProjectile(); // calling a function from a different script
            Instantiate(HitEffect, transform.position, transform.rotation); // Instantiate the prefab 'HitEffect' that will be added in Unity, in this the object's position and rotate it 
            Destroy(gameObject); // destroying the game object now (Destroing the balistic object not the entire projectile set because I want the smoke particles to stay 

            if(col.tag == "Enemy") // anything that collides with an object tagged "Enemy" then...
            {
                EnemyHealth hurtEnemy = col.gameObject.GetComponent<EnemyHealth>(); // retrieve the game object collider/reference from EnemyHealth script and...
                hurtEnemy.addDamage(HitDamage);  // adding damage to damage in EnemyHealth script, where it deducts it from enemy health
            }
        }
    }

     void OnTriggerStayr2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Explodable")) // if the game object collides with anything in the LayerMask of Explodable then...
        {
            hitDetector.stopProjectile(); // calling a function from a different script
            Instantiate(HitEffect, transform.position, transform.rotation); // Instantiate the prefab 'HitEffect' that will be added in Unity, in this the object's position and rotate it 
            Destroy(gameObject); // destroying the game object now (Destroing the balistic object not the entire projectile set because I want the smoke particles to stay 

            if (col.tag == "Enemy") // anything that collides with an object tagged "Enemy" then...
            {
                EnemyHealth hurtEnemy = col.gameObject.GetComponent<EnemyHealth>(); // retrieve the game object collider/reference from EnemyHealth script and...
                hurtEnemy.addDamage(HitDamage);  // adding damage to damage in EnemyHealth script, where it deducts it from enemy health
            }
        }
    }



}
