using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private PlayerMove myPlayer;
    Animator anim;

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
        if (healthBar.value <= 0)
        {
            // life -= 1;
            // gameObject reset
           // anim.SetBool("isDead", true);
        }
    }


	// Update is called once per frame
	void Update ()
    {

        healthText.text = currentHealth.ToString() + "  %"; 

	}
}
