using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private PlayerMove myPlayer;

    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;

    float maxHealth = 100;
    float currentHealth;


    public GameObject BloodParticle;

	// Use this for initialization
	void Start ()
    {

        healthBar.value = maxHealth;
        currentHealth = healthBar.value;

	}
	

    void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "hurt")
        {
            healthBar.value -= 1.5f;
            currentHealth = healthBar.value;
            Instantiate(BloodParticle, gameObject.transform.position, gameObject.transform.rotation);
        }

    }


	// Update is called once per frame
	void Update ()
    {

        healthText.text = currentHealth.ToString() + "  %"; 

	}
}
