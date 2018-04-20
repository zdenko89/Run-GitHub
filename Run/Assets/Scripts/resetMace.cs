using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetMace : MonoBehaviour {

       public Transform Exit;

	void Start ()
    {
		

	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "portal")
        {
            print("works");
            transform.position = Exit.transform.position;

        }
    }
}
