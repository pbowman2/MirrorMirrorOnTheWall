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


    // Use this for initialization
    void Start()
    {
        // Change size of array for your requirement
        Keywords_array = new string[5];
        Keywords_array[0] = "mirror mirror on the wall";
        Keywords_array[1] = "hello";
        Keywords_array[2] = "take a picture";
        Keywords_array[3] = "change background";
        Keywords_array[4] = "reset mirror";

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
            case "mirror mirror on the wall":
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
                    Destroy(cube);
                }
                else
                    Debug.Log("Case: Your not there");

                break;

            case "take a picture":
                // Take picture code here
                
                Application.CaptureScreenshot("Screenshot.png");
                //emails.SetActive(true);
               // while(emailsText == null)
               /* {
                    Debug.Log("EmailAddressString: " + emailsText);

                }
                */
               // GameObject.Find("EmailAddress").SetActive(true);
                //GameObject newGO = (GameObject)Instantiate(theprefab);
               // emailAddressString = GameObject.Find("EmailAddress    Text").ToString();// };
               // newGO.GetComponent<"SubmitEmail">().onClick.AddListener(action1);//find the button and set
               // Debug.Log("EmailAddressString: " + emailAddressString);

                Debug.Log("Case: Take a Picture");
                break;

            case "change background":
                // Change Background code here
                removeBackground.SetActive(true);
                Texture2D incommingImage = backgroundPan[iterator];
                Debug.Log("Iterator: " + iterator);
                Debug.Log("Background: " + incommingImage.name);
                iterator++;
                removeBackground.GetComponent<CoordinateMapperView>().setImage(incommingImage);
                removeBackground.GetComponent<CoordinateMapperView>().Start();
                Debug.Log("Case: Change Background");
                break;

            case "reset mirror":
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