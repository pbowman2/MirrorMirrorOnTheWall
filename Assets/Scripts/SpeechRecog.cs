using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;


// some code from http://gyanendushekhar.com/2016/10/11/speech-recognition-in-unity3d-windows-speech-api/
public class SpeechRecog : MonoBehaviour
{
    public GameObject BodySrcManager;
    KeywordRecognizer keywordRecognizer;
    public GameObject cube;
    public GameObject emails;
    public GameObject emailsText;
    public GameObject removeBackground;
    public Texture2D[] backgroundPan;
    public CoordinateMapperView coord;

    public AudioClip audioSound;
    // keyword array
    public string[] Keywords_array;
   // KinectInputData kinect;
    //KinectInputModule kinect;
    SkeletonBone kinect;
    Windows.Kinect.KinectSensor kn;
    private BodySourceManager bodyManager;
    private Windows.Kinect.Body[] bodies;
    public KinectInputData _inputData = new KinectInputData();
    string emailAddressString;
    public Texture2DArray images;
    public int iterator = 0;
    public int picturesTaken = 0;
    GameObject canvas;
    GameObject cover;


    // Use this for initialization
    void Start()
    {
        // Change size of array for your requirement
        Keywords_array = new string[4];
        Keywords_array[0] = "hello";
        Keywords_array[1] = "take a picture";
        Keywords_array[2] = "change background";
        Keywords_array[3] = "reset";

        cover = GameObject.Find("ScreenCover");
        // instantiate keyword recognizer, pass keyword array in the constructor
        keywordRecognizer = new KeywordRecognizer(Keywords_array);
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        // start keyword recognizer
        keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        

        switch (args.text)
        {
            case "hello":
                Debug.Log("Case: HELLLO");
                //check to see if there is a body in front of the Kinect.
                bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
                bodies = bodyManager.GetData();
                bool bodyThere = false;
                foreach (var body in bodies)
                {
                    if (body.IsTracked)
                        bodyThere = true;
                }
                if (bodyThere)
                { 
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    cover.SetActive(false);
                }
                else
                    Debug.Log("Case: Your not there");

                break;

            case "take a picture":
                // Take picture code here          
                canvas = GameObject.Find("Canvas");
                canvas.SetActive(false);
               /* HAVENT TESTED float timers = Time.time + 0.03f;
                while (Time.time != timers)
                {
                    Debug.Log("Time: " + timers);
                }*/
           /*     _inputData.WaitOverAmount = (Time.time - _inputData.HoverTime) / _waitOverTime;
                if (Time.time >= _inputData.HoverTime + _waitOverTime)
                {
                    PointerEventData lookData = GetLookPointerEventData(_inputData.GetHandScreenPosition());
                    GameObject go = lookData.pointerCurrentRaycast.gameObject;
                    ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.submitHandler);
                    // reset time
                    _inputData.HoverTime = Time.time;
                }*/
                Application.CaptureScreenshot("Screenshot" + picturesTaken + ".png");
                picturesTaken++;
                Debug.Log("Case: Take a Picture");
                canvas.SetActive(true);
                break;

            case "change background":
                // Change Background code here
                removeBackground.SetActive(true);
                Texture2D incommingImage = backgroundPan[iterator];
                Debug.Log("Iterator: " + iterator);
                Debug.Log("Background: " + incommingImage.name);
                if (iterator + 1== backgroundPan.Length)
                {
                    iterator = 0;
                }
                else
                {
                    iterator++;
                }
                removeBackground.GetComponent<CoordinateMapperView>().setImage(incommingImage);
                removeBackground.GetComponent<CoordinateMapperView>().Start();
                Debug.Log("Case: Change Background");
                break;

            case "reset":
                // Reset mirror code here
                SceneManager.LoadScene("Test3");
                Debug.Log("Case: Reset Mirror");
                break;

            default:
                Debug.Log("Case: Defualt");
                break;
        }

    }
}
