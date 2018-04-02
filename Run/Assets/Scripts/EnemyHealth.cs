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
        anim = GetComponent<Animator>();
        currentHealth = enemyMaxHealth;
        EnemyhealthSlider.maxValue = currentHealth;
        EnemyhealthSlider.value = currentHealth;
	}
	
	
	void Update ()
    {
        if (currentHealth <= 0)
        {
            Dead();
            
        }

    }

    public void addDamage(float damage)
    {

        currentHealth -= damage;
        EnemyhealthSlider.gameObject.SetActive(true); 
        EnemyhealthSlider.value = currentHealth;

        
                
    }
    public void Dead()
    {
       Instantiate(enemyDeathEffect, transform.position, transform.rotation);
       anim.SetBool("isDead", true);
       GetComponent<rangerController>().enabled = false;
       Destroy(gameObject, 5f);
        
    }
}
