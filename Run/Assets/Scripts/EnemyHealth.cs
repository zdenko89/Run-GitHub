using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {


    public float enemyMaxHealth;
    float currentHealth;

	void Start ()
    {

        currentHealth = enemyMaxHealth;

	}
	
	
	void Update ()
    {
		
        

	}

    public void addDamage(float damage)
    {

        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Kill();
        }
                
    }
    void Kill ()
    {
        Destroy(gameObject);
    }
}
