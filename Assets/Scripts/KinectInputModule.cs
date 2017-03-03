using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;

//public class Interaction : MonoBehaviour
//{
//    public GameObject BodySrcManager;
//    private GameObject handCursor; // on screen hand Cursor
//   // public JointType TrackedRight;
//    public JointType TrackedRightHand;
//    //public JointType TrackedRightThumb;
//    //public JointType TrackedRightTip;
//    public JointType TrackedRightElbow;
//    private BodySourceManager bodyManager;
//    private Body[] bodies;

//    // Use this for initialization
//    void Start()
//    {
//        if (BodySrcManager == null)
//        {
//            Debug.Log("Assign Game Object with Body Source Manager");
//        }
//        else
//        {
//            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
//        }

//        handCursor = GameObject.Find("Cursor");

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (bodyManager == null)
//        {
//            return;
//        }
//        bodies = bodyManager.GetData();

//        if (bodies == null)
//        {
//            return;
//        }

//        foreach (var body in bodies)
//        {
//            if (body == null)
//            {
//                continue;
//            }

//            if (body.IsTracked)
//            {
//                //var posRight = body.Joints[TrackedRight].Position;
//                var posRH = body.Joints[TrackedRightHand].Position;
//               // var posRT = body.Joints[TrackedRightThumb].Position;
//               // var posRTip = body.Joints[TrackedRightTip].Position;
//                var posRElbow = body.Joints[TrackedRightElbow].Position;
                
//                // move hand cursor on screen
//                if ((posRH.Y > posRElbow.Y) 
//                    && body.HandRightState == HandState.Open)
//                {
//                    handCursor.transform.position =
//                        new Vector3((posRH.X * 200f)-10, (posRH.Y * 200f)-75);
//                    //Camera.main.WorldToScreenPoint(new Vector3(posRH.X - 29, posRH.Y - 52, 0));
//                    // Draw Hand Cursor

//                }
//                else
//                {
//                    gameObject.transform.position =
//                      new Vector3(30, -55);
//                }
//            }
//            else
//            {
//                gameObject.transform.position =
//                     new Vector3(30,-55);
//            }
//        }

//    }
//}

[AddComponentMenu("Kinect/Kinect Input Module")]
[RequireComponent(typeof(EventSystem))]
public class KinectInputModule : BaseInputModule
{
    public KinectInputData _inputData = new KinectInputData();
    [SerializeField]
    private float _scrollTreshold = .5f;
    [SerializeField]
    private float _scrollSpeed = 3.5f;
    [SerializeField]
    private float _waitOverTime = 2f;

    PointerEventData _handPointerData;

    static KinectInputModule _instance = null;
    public static KinectInputModule instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(KinectInputModule)) as KinectInputModule;

                if (!_instance)
                {
                    if (EventSystem.current)
                    {
                        EventSystem.current.gameObject.AddComponent<KinectInputModule>();
                        Debug.LogWarning("Add Kinect Input Module to your EventSystem!");
                    }
                    else
                        Debug.LogWarning("Create your UI first");
                }
            }
            return _instance;
        }
    }

    /// <summary>
    ///  Call this from your Kinect body view from Update method
    /// </summary>
    /// <param name="body"></param>
    public void TrackBody(Body body)
    {
        _inputData.UpdateComponent(body);
    }

    public void NotTrackBody()
    {
        _inputData.IsTracking = false;
    }

    // get a pointer event data for a screen position
    private PointerEventData GetLookPointerEventData(Vector3 componentPosition)
    {
        if (_handPointerData == null)
        {
            _handPointerData = new PointerEventData(eventSystem);
        }
        _handPointerData.Reset();
        _handPointerData.delta = Vector2.zero;
        _handPointerData.position = componentPosition;
        _handPointerData.scrollDelta = Vector2.zero;
        eventSystem.RaycastAll(_handPointerData, m_RaycastResultCache);
        _handPointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_RaycastResultCache.Clear();
        return _handPointerData;
    }

    public override void Process()
    {
        ProcessHover();
       // ProcessPress();
       // ProcessDrag();
       // ProcessWaitOver();
    }

    /// <summary>
    /// Process hovering over component, sends pointer enter exit event to gameobject
    /// </summary>
    private void ProcessHover()
    {
        PointerEventData pointer = GetLookPointerEventData(_inputData.GetHandScreenPosition());
        var obj = _handPointerData.pointerCurrentRaycast.gameObject;
        HandlePointerExitAndEnter(pointer, obj);
        // Hover update
        _inputData.IsHovering = obj != null ? true : false;
        //if (obj != null)
        _inputData.HoveringObject = obj;
    }

    /// <summary>
    /// Used from UI hand cursor components
    /// </summary>
    /// <param name="handType"></param>
    /// <returns></returns>
    public KinectInputData GetHandData(JointType handType)
    {
        if (_inputData.trackingHandType == handType)
            return _inputData;

        return null;
    }
}

[System.Serializable]
public class KinectInputData
{
    // Which hand we are tracking
    //public KinectUIHandType trackingHandType = KinectUIHandType.Right;
    public JointType trackingHandType = JointType.HandRight;
    // We can normalize camera z position with this
    public float handScreenPositionMultiplier = 5f;
    // Is hand in pressing condition
    private bool _isPressing;//, _isHovering;
    // Hovering Gameobject, needed for WaitOver like clicking detection
    private GameObject _hoveringObject;
   
    // Hovering Gameobject getter setter, needed for WaitOver like clicking detection
    public GameObject HoveringObject
    {
        get { return _hoveringObject; }
        set
        {
            if (value != _hoveringObject)
            {
                HoverTime = Time.time;
                _hoveringObject = value;
                if (_hoveringObject == null)
                    return;
                if (_hoveringObject.GetComponent<KinectUIWaitOverButton>()) 
                    ClickGesture = KinectUIClickGesture.WaitOver;
                else
                    ClickGesture = KinectUIClickGesture.HandState;
                WaitOverAmount = 0f;
            }
        }
    }
    public HandState CurrentHandState { get; private set; }
    // Click gesture of button
    public KinectUIClickGesture ClickGesture { get; private set; }
    // Is this hand tracking started
    public bool IsTracking { get; set; }
    // Is this hand over a UI component
    public bool IsHovering { get; set; }
    // Is hand dragging a component
    public bool IsDraging { get; set; }
    // Is hand pressing a button
    public bool IsPressing
    {
        get { return _isPressing; }
        set
        {
            _isPressing = value;
            if (_isPressing)
                TempHandPosition = HandPosition;
        }
    }
    // Global position of tracked hand
    public Vector3 HandPosition { get; private set; }
    // Temporary hand position of hand, used for draging check
    public Vector3 TempHandPosition { get; private set; }
    // Hover start time, used for waitover type buttons
    public float HoverTime { get; set; }
    // Amout of wait over , between 1 - 0 , when reaches 1 button is clicked
    public float WaitOverAmount { get; set; }

    // Must be called for right hand 
    public void UpdateComponent(Body body)
    {
        HandPosition = GetVector3FromJoint(body.Joints[JointType.HandRight]);
        Debug.Log("HandPostion: " + HandPosition + "\n");
        CurrentHandState = GetStateFromJointType(body, JointType.HandRight);
        IsTracking = true;
    }
    // Converts hand position to screen coordinates
    public Vector3 GetHandScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(new Vector3(HandPosition.x, HandPosition.y, HandPosition.z - handScreenPositionMultiplier));
    }
    // Get hand state data from kinect body
    private HandState GetStateFromJointType(Body body, JointType type)
    {
        switch (type)
        {
            case JointType.HandRight:
                return body.HandRightState;
            default:
                Debug.LogWarning("Please select a hand joint, by default right hand will be used!");
                return body.HandRightState;
        }
    }
    // Get Vector3 position from Joint position
    private Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint)
    {
        
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}

public enum KinectUIClickGesture
{
    HandState, Push, WaitOver
}
