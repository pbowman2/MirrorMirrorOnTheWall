using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;
/// <summary>
/// Abstract UI component class for hand cursor objects
/// </summary>
[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public abstract class AbstractKinectUICursor : MonoBehaviour {

    [SerializeField]
    protected GameObject cursor;
    //protected JointType _handType;
    protected KinectInputData _data;
    protected Image _image;

    public virtual void Start()
    {
        cursor = GameObject.Find("Cursor");
        Setup();
    }

    protected void Setup()
    {
        _data = KinectInputModule.instance.GetHandData(JointType.HandRight);
        // Make sure we dont block raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;
        // image component
        _image = GetComponent<Image>();
    }

    public virtual void Update()
    {
        if (_data == null || !_data.IsTracking)
            return;

        ProcessData();       
    }

    public abstract void ProcessData();
}
