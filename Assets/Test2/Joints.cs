﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
//using Windows.Kinect.Toolkit.Interaction;

public class Joints : MonoBehaviour {

    public GameObject BodySrcManager;
    public JointType TrackedLeft;
    public JointType TrackedRight;
    public JointType TrackedMiddle;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public int multiplier;
    //public InteractionHandEventType HandEventType;


    // Use this for initialization
    void Start () {
        if (BodySrcManager == null)
        {
            Debug.Log("Assign Game Object with Body Source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bodyManager == null)
        {
            return;
        }
        bodies = bodyManager.GetData();
        if (bodies == null)
        {
            return;
        }
        foreach (var body in bodies)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                var posLeft = body.Joints[TrackedLeft].Position;
                var posRight = body.Joints[TrackedRight].Position;
                var posMid = body.Joints[TrackedMiddle].Position;
                gameObject.transform.position = new Vector3(posMid.X, posMid.Y);
                gameObject.transform.localScale = new Vector3(posLeft.X * 66, posLeft.Y*66);
            }
        }
		
	}
}
