using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class BackgroundPanel : MonoBehaviour
{

    public GameObject[] background;
    public GameObject curser;
    public bool pressed;
    public GameObject scrollUp;
    public GameObject scrollDown;
    public JointType TrackedRight;
    private BodySourceManager bodyManager;
    private Windows.Kinect.Body[] bodies;
    public GameObject BodySrcManager;
    public GameObject spawn;
    int backgroundActive;


    bool firstbackground = false;
    bool secondbackground = false;
    bool thirdbackground = false;
    GameObject putOnUser;
    GameObject pressingObject;
    public GameObject removeBackground;
    public Texture2D[] backgroundPan;
    public CoordinateMapperView coord;
    SkeletonBone kinect;
    Windows.Kinect.KinectSensor kn;
    public KinectInputData _inputData = new KinectInputData();
    public Texture2DArray images;
    public int iterator = 0;
    public int picturesTaken = 0;
    GameObject canvas;


    void Start()
    {
        pressed = false;

        background[0].SetActive(true);
        background[1].SetActive(true);
        background[2].SetActive(true);
        background[3].SetActive(false);
        background[4].SetActive(false);
        background[5].SetActive(false);
        background[6].SetActive(false);
        background[7].SetActive(false);
        background[8].SetActive(false);
        background[9].SetActive(false);
        background[10].SetActive(false);
        background[11].SetActive(false);
        background[12].SetActive(false);
        background[13].SetActive(false);
        background[14].SetActive(false);

        backgroundActive = 0;
        pressingObject = GameObject.Find("IsPressed");
    }
    void Update()
    {

        pressingObject = GameObject.Find("IsPressed");
        pressed = pressingObject.GetComponent<Pressing>().handpressed;

        scrollUp.SetActive(true);
        scrollDown.SetActive(true);

        if (background[0].activeSelf == true)
        {
            scrollUp.SetActive(false);
        }
        else if (background[3].activeSelf == true)
        {
            backgroundActive = 3;

        }
        else if (background[6].activeSelf == true)
        {
            backgroundActive = 3;

        }
        else if (background[9].activeSelf == true)
        {
            backgroundActive = 3;

        }
        else if (background[12].activeSelf == true)
        {
            backgroundActive = 3;
            scrollDown.SetActive(false);

        }

        //Debug.Log("Cursur: " + curser.transform.position.x + " : " + curser.transform.position.y);

        if (curser.transform.position.x < 992 && curser.transform.position.x > 801 && pressed == true)
        {
            pressingObject.GetComponent<Pressing>().handpressed = false;
            pressed = false;
            if (curser.transform.position.y < 1407 && curser.transform.position.y > 1250)
            {
                if (firstbackground == false)
                {
                    removeBackground.SetActive(true);
                    Texture2D incommingImage = backgroundPan[backgroundActive];
                    Debug.Log("Background: " + incommingImage.name);
                    removeBackground.GetComponent<CoordinateMapperView>().setImage(incommingImage);
                    removeBackground.GetComponent<CoordinateMapperView>().Start();
                    Debug.Log("Case: Change Background");
                    firstbackground = true;
                    secondbackground = false;
                    thirdbackground = false;
                    // Debug.Log("Clothing " + putOnUser.transform.position.x + " : " + putOnUser.transform.position.y);
                    //Debug.Log("1: ");
                }

            }
            else if (curser.transform.position.y < 1205 && curser.transform.position.y > 1050)
            {
                if (secondbackground == false)
                {
                    removeBackground.SetActive(true);
                    Texture2D incommingImage = backgroundPan[backgroundActive + 1];
                    Debug.Log("Background: " + incommingImage.name);
                    removeBackground.GetComponent<CoordinateMapperView>().setImage(incommingImage);
                    removeBackground.GetComponent<CoordinateMapperView>().Start();
                    Debug.Log("Case: Change Background");
                    firstbackground = false;
                    secondbackground = true;
                    thirdbackground = false;
                    Debug.Log("2: ");
                }

            }
            else if (curser.transform.position.y < 1000 && curser.transform.position.y > 884)
            {
                if (spawn == null || thirdbackground == false)
                {
                    removeBackground.SetActive(true);
                    Texture2D incommingImage = backgroundPan[backgroundActive + 2];
                    Debug.Log("Background: " + incommingImage.name);
                    removeBackground.GetComponent<CoordinateMapperView>().setImage(incommingImage);
                    removeBackground.GetComponent<CoordinateMapperView>().Start();
                    Debug.Log("Case: Change Background");
                    firstbackground = false;
                    secondbackground = false;
                    thirdbackground = true;
                    Debug.Log("3: ");

                }
            }
        }



    }

    public void PressUpButton()
    {
        backgroundActive -= 3;

        background[backgroundActive - 1].SetActive(false);
        background[backgroundActive - 2].SetActive(false);
        background[backgroundActive - 3].SetActive(false);
        background[backgroundActive].SetActive(true);
        background[backgroundActive + 1].SetActive(true);
        background[backgroundActive + 2].SetActive(true);


        if (backgroundActive == 0)
        {
            scrollUp.SetActive(false);
            scrollDown.SetActive(true);
        }

    }

    public void PressDownButton()
    {
        backgroundActive += 3;

        background[backgroundActive - 1].SetActive(false);
        background[backgroundActive - 2].SetActive(false);
        background[backgroundActive - 3].SetActive(false);
        background[backgroundActive].SetActive(true);
        background[backgroundActive + 1].SetActive(true);
        background[backgroundActive + 2].SetActive(true);

        if (backgroundActive == 12)
        {
            scrollUp.SetActive(true);
            scrollDown.SetActive(false);
        }

    }
}

