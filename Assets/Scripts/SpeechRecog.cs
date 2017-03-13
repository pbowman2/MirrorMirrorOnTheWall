using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
// some code from http://gyanendushekhar.com/2016/10/11/speech-recognition-in-unity3d-windows-speech-api/
public class SpeechRecog : MonoBehaviour
{

    KeywordRecognizer keywordRecognizer;
    public GameObject cube;
    public AudioClip audioSound;
    // keyword array
    public string[] Keywords_array;

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
        switch(args.text)
        {
            case "mirror mirror on the wall":
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                Destroy(cube);
                break;

            case "take a picture":
                // Take picture code here
                Debug.Log("Case: Take a Picture");
                break;

            case "change background":
                // Change Background code here
                Debug.Log("Case: Change Background");
                break;

            case "reset mirror":
                // Reset mirror code here
                Debug.Log("Case: Reset Mirror");
                break;

            default:
                Debug.Log("Case: Defualt");
                break;
        }

    }
}