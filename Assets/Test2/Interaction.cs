using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
//using Windows.Kinect.Toolkit.Interaction;

public class Interaction : MonoBehaviour
{

    public GameObject BodySrcManager;
    public JointType TrackedRight;
    public JointType TrackedRightHand;
    public JointType TrackedRightThumb;
    public JointType TrackedRightTip;
    private BodySourceManager bodyManager;
    private Body[] bodies;

    //public InteractionHandEventType HandEventType;


    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
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

                var posRight = body.Joints[TrackedRight].Position;
                var posRH = body.Joints[TrackedRightHand].Position;
                var posRT = body.Joints[TrackedRightThumb].Position;
                var posRTip = body.Joints[TrackedRightTip].Position;
               //  if (posRH.X < posRight.X )
                  {
                      gameObject.transform.position = new Vector3((posRH.X * 10f) + 510, (posRH.Y * 20f) + 960);

                  }/*
                //  else
                  {
                      //gameObject.transform.position = new Vector3(posRH.X, posRight.X);
                      gameObject.transform.position = new Vector3(-4, -4);

                  }*/
                 /*
                if (posRight.X < posRH.X)
                {
                    while (posRight.X - posRH.X < 0.2)
                    {
                        gameObject.transform.position = new Vector3(posRH.X, posRight.X);
                    }
                }
                else
                {
                    gameObject.transform.position = new Vector3(-4, -4);
                    
                }*/
            }
        }

    }
}
