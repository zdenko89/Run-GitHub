using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {


    public Transform target;
    Vector3 velocity = Vector3.zero;
    public float smothTime = .15f;


    public bool YmaxEnabled = false; // enable and set the y max value for the camera
    public float YmaxValue = 0;

    public bool YminEnabled = false; // enable and set the y min value for the camera
    public float YminValue = 0;

    public bool XmaxEnabled = false; // enable and set the x max value for the camera
    public float XmaxValue = 0;

    public bool XminEnabled = false; // enable and set the x min value for the camera
    public float XminValue = 0;



    void FixedUpdate()
    {
        Vector3 targetpos = target.position; // target position

        targetpos.z = transform.position.z; // this alligns the camera and target in z position

        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocity, smothTime);

        //This clamps the movement of the camera. The values are set in the scene, the values can be retrieved from the camera position and added to the public floats.
        //This is a good method for the camera to stay restricted to a certain area within the scene.
        //Vertical
        if (YmaxEnabled && YminEnabled)
        {
            targetpos.y = Mathf.Clamp(target.position.y, YminValue, YmaxValue);
        }
        else if (YminEnabled)
        {
            targetpos.y = Mathf.Clamp(target.position.y, YminValue, target.position.y);
        }
        else if (YmaxEnabled)
        {
            targetpos.y = Mathf.Clamp(target.position.y, target.position.y, YmaxValue);
        }

        //Horizontal
        if (XmaxEnabled && XminEnabled)
        {
            targetpos.x = Mathf.Clamp(target.position.x, XminValue, XmaxValue);
        }
        else if (XminEnabled)
        {
            targetpos.x = Mathf.Clamp(target.position.x, XminValue, target.position.x);
        }
        else if (XmaxEnabled)
        {
            targetpos.x = Mathf.Clamp(target.position.x, target.position.x, XmaxValue);
        }

    }

}
