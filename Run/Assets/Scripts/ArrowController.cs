using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    Rigidbody2D Arrow;
    public float ArrowSpeed;

    void Start()
    {

    }

    void Awake()
    {
        shoot();
    }

    void Update()
    {

    }

    public void stopArrow()
    {
        Arrow.velocity = new Vector2(0, 0);
    }

    public void shoot()
    {

        Arrow = GetComponent<Rigidbody2D>();

        if (transform.localRotation.z > 0) // 
        {
            Arrow.AddForce(new Vector2(-1, 0) * ArrowSpeed, ForceMode2D.Impulse);
        }
        else
        {
            Arrow.AddForce(new Vector2(1, 0) * ArrowSpeed, ForceMode2D.Impulse);
        }

    }
}
