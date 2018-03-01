using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    public float speed = 0.5f;
    Vector3 start;

	void Start ()
    {
        start = transform.position;	
	}
	
	
	void Update ()
    {
            
        transform.Translate(new Vector3 (-1, 0, 0) * speed * Time.deltaTime );

        
	}
}
