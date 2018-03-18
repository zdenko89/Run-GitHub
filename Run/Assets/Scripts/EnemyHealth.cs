using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public GameObject enemyDeathEffect;

    public float enemyMaxHealth;

    public Slider EnemyhealthSlider;

    float currentHealth;

    Animator anim;

	void Start ()
    {

        currentHealth = enemyMaxHealth;
        EnemyhealthSlider.maxValue = currentHealth;
        EnemyhealthSlider.value = currentHealth;
	}
	
	
	void Update ()
    {
		
        
	}

    public void addDamage(float damage)
    {

        currentHealth -= damage;
        EnemyhealthSlider.gameObject.SetActive(true); 
        EnemyhealthSlider.value = currentHealth;

        if(currentHealth <= 0)
        {
            Dead();
        }
                
    }
    public void Dead()
    {
       Instantiate(enemyDeathEffect, transform.position, transform.rotation);
       anim.SetBool("isDead", true);
       Destroy(gameObject);
        
    }
}
