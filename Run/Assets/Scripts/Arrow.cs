using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float speed;
    private Transform Player;
    private Vector2 target;
    public GameObject ImpactEffect;
    public float HitDamage;
   

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(Player.position.x, Player.position.y);
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyArrow();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            DestroyArrow();
        }
        
    }

    void DestroyArrow()
    {
        Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
