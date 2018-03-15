using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrow : MonoBehaviour {

    public float aliveTime;

    void Start()
    {

    }

    void Awake()
    {
        Destroy(gameObject, aliveTime);
    }


    void Update()
    {

    }
}
