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

    public AudioClip playerDied;
    public AudioClip playerHurt;
    AudioSource PlayerAudio; 


    float damage = 1.5f;
    

    float maxHealth = 100;
    float currentHealth;
   
    public GameObject BloodParticle;

    
    PlayerController pc;
    rangerController rangerController;

    PlayerLives lives;
    PlayerLives PlayerLives;
    CheckPoints checkPoints; 



    void Start ()
    {

        
        lives = FindObjectOfType<PlayerLives>();
        PlayerLives = GetComponent<PlayerLives>();

        checkPoints = GetComponent<CheckPoints>();
                      
        rangerController = GetComponent<rangerController>();

        pc = GetComponent<PlayerController>();

        PlayerAudio = GetComponent<AudioSource>();  

        anim = GetComponent<Animator>();

        healthBar.value = maxHealth;
        currentHealth = healthBar.value;

	}
	
    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;

        
    }

    public void addHealth(float healthAmount)
    {
        currentHealth = currentHealth + healthAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "hurt")
        {
            healthBar.value -= damage;
            currentHealth = healthBar.value;
            Instantiate(BloodParticle, gameObject.transform.position, gameObject.transform.rotation);

            //PlayerAudio.clip = playerHurt;
            //PlayerAudio.Play();

            PlayerAudio.PlayOneShot(playerHurt);
        }
        
    }


	// Update is called once per frame
	void Update ()
    {

        healthText.text = currentHealth.ToString() + "  %";

        if (lives.lifeCounter > 0 && currentHealth <= 0) // if the amount of lives is more than 0 and current health has hit 0 then ... 
        {
            pc.transform.position = pc.respawnPoint; // respawning the player back to the last checkpoint
            currentHealth = maxHealth; // updating the current health + text
            healthBar.value = maxHealth; // updating the slider's health value 
            lives.RemoveLife();

        }


        //if (currentHealth <= 0) // if the current health is less or equal to 0
        //{
        //   // if(PlayerLives.lifeCounter > 0)
        //   // {
               
        //    // }
        //    // else if (PlayerLives.lifeCounter == 0)

                
        //        anim.SetBool("isDead", true); // plays the dead animation 
        //        deadUI.gameObject.SetActive(true); // show the dead menu 
        //        PlayerAudio.PlayOneShot(playerDied); // play the audio clip for player dying
        //        GetComponent<PlayerController>().enabled = false; // stops the player movement
        //        //GameObject<rangerController>().enabled = false; // stop the ranger from staying active when player is dead
        //        GameObject.Find("Archer Enemy").GetComponent<rangerController>().enabled = false; // attempt to stop the ranger from staying active
        //        GameObject.Find("Archer Enemy 2").GetComponent<rangerController>().enabled = false;
        //        GameObject.Find("Archer Enemy 3").GetComponent<rangerController>().enabled = false;
        //        GameObject.Find("Archer Enemy 4").GetComponent<rangerController>().enabled = false;

            

        //}
    }
}
