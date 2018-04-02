using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    
    Animator anim;
    [SerializeField]
    GameObject deadUI;
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;

    float damage = 1.5f;

    float maxHealth = 100;
    float currentHealth;


    public GameObject BloodParticle;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        healthBar.value = maxHealth;
        currentHealth = healthBar.value;

	}
	
    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;

        
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "hurt")
        {
            healthBar.value -= damage;
            currentHealth = healthBar.value;
            Instantiate(BloodParticle, gameObject.transform.position, gameObject.transform.rotation);
        }
        
    }


	// Update is called once per frame
	void Update ()
    {

        healthText.text = currentHealth.ToString() + "  %";


        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true); // plays the dead animation 
            GetComponent<PlayerController>().enabled = false; // stops the player movement
            deadUI.gameObject.SetActive(true);
            GetComponent<rangerController>().enabled = false; // stop the ranger from staying active when player is dead
        }
    }
}
