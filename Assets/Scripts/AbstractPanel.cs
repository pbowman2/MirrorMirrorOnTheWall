﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public abstract class AbstractPanel : MonoBehaviour
{

    protected GameObject panel;
    protected KinectInputData _data;
    protected float currentPosX;
    protected float handPosX;
    protected float stopRightX, stopLeftX;  // stops the menu from going off the screen

    // Use this for initialization
    public virtual void Start ()
    {
        setPanel();
        _data = KinectInputModule.instance.GetHandData(JointType.HandRight);
    }
	
	// Update is called once per frame
	public virtual void Update () {
        //get position of hand on the sreen and move the panel according to the hand
        currentPosX = panel.transform.position.x;
        if (_data.GetHandScreenPosition().x
            > 700f && _data.GetHandScreenPosition().y < 1300f)
        {
            if (panel.transform.position.x >= stopRightX)
            {
                currentPosX -= 1;
                panel.transform.position = new Vector3(currentPosX, panel.transform.position.y, panel.transform.position.z);
            }
        }
        else if (_data.GetHandScreenPosition().x
            < 300f && _data.GetHandScreenPosition().y < 1300f)
        {
            if (panel.transform.position.x <= stopLeftX)
            {
                currentPosX += 1;
                panel.transform.position = new Vector3(currentPosX, panel.transform.position.y, panel.transform.position.z);
            }
        }
    }

    public abstract void setPanel();

}
