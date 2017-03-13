/* * 
 * Most code from https://nevzatarman.com/2015/07/13/kinect-hand-cursor-for-unity3d/
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(.8f, .8f, .8f);
    
    // help with smoothing the cursor movement
    float currentX;
    float currentY;
    float targetX;
    float targetY;
    float diffX;
    float diffY;
    float delta;

    private Vector3 _initScale;

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        _image.color = new Color(1f, 1f, 1f, 0f);
        delta = .1f; // 0 = no movement, 1 = move directly to target point. 
        currentX = 0f;
        currentY = 0f;
    }

    public override void ProcessData()
    {
        // update pos
        targetX = _data.GetHandScreenPosition().x; 
        targetY = _data.GetHandScreenPosition().y; 

        diffX = targetX - currentX;
        diffY = targetY - currentY;
        
        currentX = currentX + delta * diffX;
        currentY = currentY + delta * diffY;

        cursor.transform.position = new Vector3(currentX, currentY);//_data.GetHandScreenPosition();
        //Debug.Log("Process: " + _data.GetHandScreenPosition());
        if (_data.IsPressing)
        {
            _image.color = clickColor;
            _image.transform.localScale = clickScale;
            return;
        }
        if (_data.IsHovering)
        {
            _image.color = hoverColor;
        }
        else
        {
            _image.color = normalColor;
        }

        cursor.transform.localScale = _initScale;
    }
}
