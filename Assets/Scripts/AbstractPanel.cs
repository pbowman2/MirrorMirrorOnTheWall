using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public abstract class AbstractPanel : MonoBehaviour
{

    protected GameObject panel;
    protected KinectInputData _data;
    protected float currentPosX;
    protected float handPosX;

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
            currentPosX += 5;
            panel.transform.position = new Vector3(currentPosX, panel.transform.position.y);
        }
        else if (_data.GetHandScreenPosition().x 
            < 300f && _data.GetHandScreenPosition().y < 1300f)
        {
            currentPosX -= 5;
            panel.transform.position = new Vector3(currentPosX, panel.transform.position.y);
        }
    }

    public abstract void setPanel();

}
