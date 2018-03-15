using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{

    public float damage;
    ArrowController ArrowController; // reference to script
    public GameObject HitEffect;


    void Start()
    {

    }

    void Awake()
    {
        ArrowController = GetComponentInParent<ArrowController>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) // if the game object collides with anything in the LayerMask of Explodable then...
        {
            ArrowController.stopArrow(); // calling a function from a different script
            Instantiate(HitEffect, transform.position, transform.rotation); // Instantiate the prefab 'HitEffect' that will be added in Unity, in this the object's position and rotate it 
            Destroy(gameObject); // destroying the game object now (Destroing the balistic object not the entire projectile set because I want the smoke particles to stay 

            if (col.tag == "Player") // anything that collides with an object tagged "Enemy" then...
            {
                PlayerHealth hurtPlayer = col.gameObject.GetComponent<PlayerHealth>(); // retrieve the game object collider/reference from Player health script and...
                hurtPlayer.addDamage(damage);  // adding damage to damage to do function in Player health script, where it deducts it from the player's health
            }
        }
    }

    void OnTriggerStayr2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) // if the game object collides with anything in the LayerMask of Explodable then...
        {
            ArrowController.stopArrow(); // calling a function from a different script
            Instantiate(HitEffect, transform.position, transform.rotation); // Instantiate the prefab 'HitEffect' that will be added in Unity, in this the object's position and rotate it 
            Destroy(gameObject); // destroying the game object now (Destroing the balistic object not the entire projectile set because I want the smoke particles to stay 

            if (col.tag == "Player") // anything that collides with an object tagged "Enemy" then...
            {
                PlayerHealth hurtPlayer = col.gameObject.GetComponent<PlayerHealth>(); // retrieve the game object collider/reference from Player health script and...
                hurtPlayer.addDamage(damage);  // adding damage to damage to do function in Player health script, where it deducts it from the player's health
            }
        }
    }



}
