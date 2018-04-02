using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBallController : MonoBehaviour {

	public float cannonSpeedHigh;
    public float cannonSpeedLow;
    Rigidbody2D cannonBall;
    public float cannonBallAngle;
    public float cannonTorque;

	void Start ()
    {
        cannonBall = GetComponent<Rigidbody2D>();
        cannonBall.AddForce(new Vector2(Random.Range(-cannonBallAngle, cannonBallAngle), Random.Range(cannonSpeedLow, cannonSpeedHigh)), ForceMode2D.Impulse);
        // adding random force in the x and y as well as adding impurlse force which is explosive.

        cannonBall.AddTorque(Random.Range(-cannonTorque, cannonTorque));
    }
	
	
	void Update ()
    {
		
	}
}
