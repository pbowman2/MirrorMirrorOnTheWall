using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
//using Windows.Kinect.Toolkit.Interaction;

public class Joints : MonoBehaviour {

    GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    float multiplier;
    float multiplier2;
    //public InteractionHandEventType HandEventType;


    // Use this for initialization
    void Start () {
        BodySrcManager = GameObject.Find("BodySourceManager");
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
                var posLeft = body.Joints[JointType.ShoulderLeft].Position;
                var posRight = body.Joints[JointType.ShoulderRight].Position;
                var posMid = body.Joints[JointType.SpineMid].Position;
                var posBot = body.Joints[JointType.SpineBase].Position;
                var posTop = body.Joints[JointType.SpineBase].Position;
                var mult = System.Math.Abs(posMid.X - 5) * 10;
                gameObject.transform.position = new Vector3((posMid.X * mult)+5, (posMid.Y * mult), 80F);
                Debug.Log("PosLeft :" + posLeft.X + " posRight: " + posRight.X);
                //multiplier = (System.Math.Abs(posLeft.X - posRight.X) * 400);
                //   multiplier2 = gameObject.transform.position.X
                var xSqaure = (double)System.Math.Pow(posLeft.X - posRight.X, 2);
                var ySqaure = (double)System.Math.Pow(posLeft.Y - posRight.Y, 2);
                var zSqaure = (double)System.Math.Pow(posLeft.Z - posRight.Z, 2);
                multiplier = (float)System.Math.Sqrt((xSqaure + ySqaure + zSqaure)) * 300;
             //   gameObject.transform.position.X
                Debug.Log("mutliplier: " + multiplier);
                gameObject.transform.localScale = new Vector3(multiplier, multiplier, multiplier);
            }
        }
		
	}
}
