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
    }

    public override void ProcessData()
    {
        // update pos
        currentX = _data.GetHandScreenPosition().x;
        currentY = _data.GetHandScreenPosition().y;
        targetX = _image.transform.position.x;
        targetY = _image.transform.position.y;

        float diffX = targetX - currentX;
        float diffY = targetY - currentY;
        float delta = 0.5f; // 0 = no movement, 1 = move directly to target point. 

        currentX = currentX + delta * diffX;
        currentY = currentY + delta * diffY;

        cursor.transform.position = _data.GetHandScreenPosition();
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
