using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
// code from http://gyanendushekhar.com/2016/10/11/speech-recognition-in-unity3d-windows-speech-api/
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
        //if(args == keywordRecognizer)
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(cube);
    }
}