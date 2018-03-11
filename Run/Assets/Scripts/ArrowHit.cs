using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour {

    public float HowMuchDamage;
    public GameObject impactEffect;
    ArrowController arrowController;

    void Start ()
    {
		
	}

    void Awake()
    {
        arrowController = GetComponent<ArrowController>();
    }


    void Update ()
    {
		
	}
}
