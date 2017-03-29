using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class ShirtPanel : MonoBehaviour
{
    public GameObject[] shirts;
    public GameObject curser;
    public bool pressed;
    public GameObject scrollUp;
    public GameObject scrollDown;
    public JointType TrackedRight;
    private BodySourceManager bodyManager;
    private Windows.Kinect.Body[] bodies;
    public GameObject BodySrcManager;
    public GameObject spawn;
    int shirtsActive;


    bool firstShirt = false;
    bool secondShirt = false;
    bool thirdShirt = false;
    GameObject putOnUser;
    GameObject pressingObject;
    // Set panel
    /*  public override void setPanel()
      {
          panel = GameObject.Find("ShirtPanel");
          stopRightX = -330f;
          stopLeftX = 2566f;
      }*/

    void Start()
    {
        pressed = false;

        shirts[0].SetActive(true);
        shirts[1].SetActive(true);
        shirts[2].SetActive(true);
        shirts[3].SetActive(false);
        shirts[4].SetActive(false);
        shirts[5].SetActive(false);
        shirts[6].SetActive(false);
        shirts[7].SetActive(false);
        shirts[8].SetActive(false);
        shirts[9].SetActive(false);
        shirts[10].SetActive(false);

        shirtsActive = 0;
        pressingObject = GameObject.Find("IsPressed");

    }
    void Update()
    {
        pressingObject = GameObject.Find("IsPressed");
        pressed = pressingObject.GetComponent<Pressing>().handpressed;

        scrollUp.SetActive(true);
        scrollDown.SetActive(true);

        if (shirts[0].activeSelf == true)
        {
            scrollUp.SetActive(false);
        }
        else if(shirts[3].activeSelf == true)
        {
            shirtsActive = 3;
        }
        else if(shirts[6].activeSelf == true)
        {
            shirtsActive = 6;
        }
        else if(shirts[9].activeSelf == true)
        {
            shirtsActive = 9;
            scrollDown.SetActive(false);

        }

        //Debug.Log("Cursur: " + curser.transform.position.x + " : " + curser.transform.position.y);

        if (curser.transform.position.x < 992 && curser.transform.position.x > 740 && pressed == true)
        {
            pressingObject.GetComponent<Pressing>().handpressed = false;
            pressed = false;

            if (curser.transform.position.y < 1407 && curser.transform.position.y > 1250)
            {
                if (spawn == null || firstShirt == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(shirts[shirtsActive]); //cloning
                    firstShirt = true;
                    secondShirt = false;
                    thirdShirt = false;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                    // Debug.Log("Clothing " + putOnUser.transform.position.x + " : " + putOnUser.transform.position.y);
                    //Debug.Log("1: ");
                }

            }
            else if (curser.transform.position.y < 1205 && curser.transform.position.y > 1050)
            {
                if (spawn == null || secondShirt == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(shirts[shirtsActive + 1]);
                    //putOnUser = spawn;
                    //putputOnUser.transform.position = shirts[shirtsActive].transform.position;
                    firstShirt = false; 
                    secondShirt = true;
                    thirdShirt = false;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                    // Debug.Log("Clothing " + putOnUser.transform.position.x + " : " + putOnUser.transform.position.y);
                    Debug.Log("2: ");
                }

            }
            else if (curser.transform.position.y < 1000 && curser.transform.position.y > 884)
            {
                if (spawn == null || thirdShirt == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(shirts[shirtsActive+2]);
                    firstShirt = false;
                    secondShirt = false;
                    thirdShirt = true;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                    Debug.Log("3: ");

                }
            }
        }
        

        
    }

    public void PressUpButton()
    {
        shirtsActive -= 3;
        shirts[shirtsActive].SetActive(true);
        shirts[shirtsActive + 1].SetActive(true);
        shirts[shirtsActive + 2].SetActive(true);
        shirts[shirtsActive + 3].SetActive(false);
        shirts[shirtsActive + 4].SetActive(false);
        shirts[shirtsActive + 5].SetActive(false);
        if (shirtsActive == 0)
        {
            scrollUp.SetActive(false);
            scrollDown.SetActive(true);
        }

    }

    public void PressDownButton()
    {
        shirtsActive += 3;
        shirts[shirtsActive].SetActive(true);
        shirts[shirtsActive + 1].SetActive(true);
        if (shirtsActive != 9)
        {
            shirts[shirtsActive + 2].SetActive(true);
        }
        else
        {
            scrollUp.SetActive(true);
            scrollDown.SetActive(false);
        }
        shirts[shirtsActive - 1].SetActive(false);
        shirts[shirtsActive - 2].SetActive(false);
        shirts[shirtsActive - 3].SetActive(false);

    }
}
