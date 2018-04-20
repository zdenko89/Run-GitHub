using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    // Enemy health script

    public GameObject enemyDeathEffect;

    public float enemyMaxHealth;

    public Slider EnemyhealthSlider;

    float currentHealth;

    Animator anim;

    public bool DeadNow;
    public GameObject heart;

	void Start ()
    {
        anim = GetComponent<Animator>();
        currentHealth = enemyMaxHealth;
        EnemyhealthSlider.maxValue = currentHealth;
        EnemyhealthSlider.value = currentHealth;
	}
	
	
	void Update ()
    {
        if (currentHealth <= 0) // if the enemy dies, health goes to zero then ... 
        {
            Dead(); // activate the dead function
            isDead(); // and isDead function
        }

    }

    public void addDamage(float damage) // this function adds damage to the enemy, removes health.
    {

        currentHealth -= damage; // remove some health
        EnemyhealthSlider.gameObject.SetActive(true);  // update the UI slider
        EnemyhealthSlider.value = currentHealth; 

        
                
    }
    public void Dead() // cannot show the death animation or death effect because if the enemy object isn't destroyed right away then
                       // it will instantiate the heart several times, meaning the player will pick up more health.
    {
        Instantiate(enemyDeathEffect, transform.position, transform.rotation); // play the death effect
       anim.SetBool("isDead", true); // animate the death 
       GetComponent<rangerController>().enabled = false; // stop the range controller script, this will stop the object from functioning
       Destroy(gameObject); // destroy the enemy object
       DeadNow = true; // set the deadnow bool to true
        
    }

    public void isDead()
    {
        if (DeadNow) // if this bool is true then ... 
        {
            Instantiate(heart, transform.position, transform.rotation); // instantiate the heart at the enemy's position.
        }
    }
}
