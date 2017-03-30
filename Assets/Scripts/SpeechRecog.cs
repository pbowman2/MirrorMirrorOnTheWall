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
    public GameObject kinecting;
    GameObject shirtsB;
    GameObject suitsB;
    GameObject pantsB;
    GameObject backgroundB;


    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        shirtsB = GameObject.Find("bShirts");
        suitsB = GameObject.Find("bSuits");
        pantsB = GameObject.Find("bPants");
        backgroundB = GameObject.Find("bBackground");

        // Change size of array for your requirement
        Keywords_array = new string[4];
        Keywords_array[0] = "wall";
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
            case "wall":
                Debug.Log("Case: Wall");
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
                    shirtsB.SetActive(true);
                    suitsB.SetActive(true);
                    pantsB.SetActive(true);
                    backgroundB.SetActive(true);
                    kinecting.GetComponent<BodySourceView>().active = true;
                }
                else
                    Debug.Log("Case: Your not there");

                break;

            case "take a picture":
                // Take picture code here          
                shirtsB.SetActive(false);
                suitsB.SetActive(false);
                pantsB.SetActive(false);
                //backgroundB.SetActive(false);
                Application.CaptureScreenshot("Screenshot" + picturesTaken + ".png");
                picturesTaken++;
                Debug.Log("Case: Take a Picture");
                shirtsB.SetActive(true);
                suitsB.SetActive(true);
                pantsB.SetActive(true);
                backgroundB.SetActive(true);
                kinecting.GetComponent<BodySourceView>().active = true;
                break;

            case "change background":
                // Change Background code here
                removeBackground.SetActive(true);
                Texture2D incommingImage = backgroundPan[iterator];
                Debug.Log("Iterator: " + iterator);
                Debug.Log("Background: " + incommingImage.name);
                if (iterator + 1 == backgroundPan.Length)
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
    void CanvasCall()
    {
        canvas.SetActive(true);
    }
}
