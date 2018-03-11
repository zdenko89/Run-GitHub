using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {


    Rigidbody2D arrow;
    public float ArrowSpeed;

    void Start()
    {

    }

    void awake()
    {
        moveArrow();
    }	
	void Update ()
    {
		
	}

    public void stopArrow()
    {
        arrow.velocity = new Vector2(0,0); // this will remove all speed for the arrow on impact
    }

    public void moveArrow()
    {
        arrow = GetComponent<Rigidbody2D>(); // whatever this script is attached to, arrow is the component of it

        if (transform.localRotation.z > 0) 
        {
            arrow.AddForce(new Vector2(-1, 0) * ArrowSpeed, ForceMode2D.Impulse);
        }
        else
        {
            arrow.AddForce(new Vector2(1, 0) * ArrowSpeed, ForceMode2D.Impulse);
        }
    }
}
