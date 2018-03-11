using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

    public bool facingRight = true;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

   
    void flips()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }
}
