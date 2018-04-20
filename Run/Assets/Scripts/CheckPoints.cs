using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    public Sprite redFlag;
    public Sprite blueFlag;
    private SpriteRenderer checkpointRenderer;
    public bool touchedCheckpoint;
    

	void Start ()
    {

        checkpointRenderer = GetComponent<SpriteRenderer>();
        
	}
	
	
	void Update ()
    {
		


	}

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.tag == "Player")
        {
            checkpointRenderer.sprite = blueFlag;
            touchedCheckpoint = true;
        }

    }
}
