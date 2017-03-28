using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class SuitPanel : MonoBehaviour
{
    // Set Panel
    /*public override void setPanel()
    {
        panel = GameObject.Find("SuitPanel");
        stopLeftX = 3082f;
        stopRightX = 2639f;
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using Windows.Kinect;*/

    public GameObject[] suits;
        public GameObject curser;
        public bool pressed;
        public GameObject scrollUp;
        public GameObject scrollDown;
        public JointType TrackedRight;
        private BodySourceManager bodyManager;
        private Windows.Kinect.Body[] bodies;
        public GameObject BodySrcManager;
        public GameObject spawn;
        int suitsActive;


        bool firstSuit = false;
        bool secondSuit = false;
        bool thirdSuit = false;
        GameObject putOnUser;
        GameObject pressingObject;

        // Set panel
        /* public override void setPanel()
         {
             panel = GameObject.Find("SuitPanel");
             stopRightX = 2536f;
             stopLeftX = 3113f;
         }*/


        void Start()
        {
            pressed = false;

            suits[0].SetActive(true);
            suits[1].SetActive(true);
            suits[2].SetActive(true);
            suits[3].SetActive(false);
            suits[4].SetActive(false);
            suits[5].SetActive(false);

            suitsActive = 0;
            pressingObject = GameObject.Find("IsPressed");
        }
        void Update()
        {
            pressingObject = GameObject.Find("IsPressed");
            pressed = pressingObject.GetComponent<Pressing>().handpressed;

            scrollUp.SetActive(true);
            scrollDown.SetActive(true);

            if (suits[0].activeSelf == true)
            {
                scrollUp.SetActive(false);
            }
            else if (suits[3].activeSelf == true)
            {
                suitsActive = 3;
                scrollDown.SetActive(false);

            }

            //Debug.Log("Cursur: " + curser.transform.position.x + " : " + curser.transform.position.y);

            if (curser.transform.position.x < 992 && curser.transform.position.x > 740 && pressed == true)
            {
                pressingObject.GetComponent<Pressing>().handpressed = false;
                pressed = false;
                if (curser.transform.position.y < 1407 && curser.transform.position.y > 1250)
                {
                    if (spawn == null || firstSuit == false)
                    {
                        Destroy(spawn);
                        spawn = null;
                        spawn = (GameObject)Instantiate(suits[suitsActive]); //cloning
                        firstSuit = true;
                        secondSuit = false;
                        thirdSuit = false;
                        spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                        // Debug.Log("Clothing " + putOnUser.transform.position.x + " : " + putOnUser.transform.position.y);
                        //Debug.Log("1: ");
                    }

                }
                else if (curser.transform.position.y < 1205 && curser.transform.position.y > 1050)
                {
                    if (spawn == null || secondSuit == false)
                    {
                        Destroy(spawn);
                        spawn = null;
                        spawn = (GameObject)Instantiate(suits[suitsActive + 1]);
                        //putOnUser = spawn;
                        //putputOnUser.transform.position = suits[suitsActive].transform.position;
                        firstSuit = false;
                        secondSuit = true;
                        thirdSuit = false;
                        spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                        // Debug.Log("Clothing " + putOnUser.transform.position.x + " : " + putOnUser.transform.position.y);
                        Debug.Log("2: ");
                    }

                }
                else if (curser.transform.position.y < 1000 && curser.transform.position.y > 884)
                {
                    if (spawn == null || thirdSuit == false)
                    {
                        Destroy(spawn);
                        spawn = null;
                        spawn = (GameObject)Instantiate(suits[suitsActive + 2]);
                        firstSuit = false;
                        secondSuit = false;
                        thirdSuit = true;
                        spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                        Debug.Log("3: ");

                    }
                }
            }



        }

        public void PressUpButton()
        {
            suits[0].SetActive(true);
            suits[1].SetActive(true);
            suits[2].SetActive(true);
            suits[3].SetActive(false);
            suits[4].SetActive(false);
            suits[5].SetActive(false);

            scrollUp.SetActive(false);
            scrollDown.SetActive(true);


        }

        public void PressDownButton()
        {
            suitsActive += 3;

            suits[0].SetActive(false);
            suits[1].SetActive(false);
            suits[2].SetActive(false);
            suits[3].SetActive(true);
            suits[4].SetActive(true);
            suits[5].SetActive(true);
            scrollUp.SetActive(true);
            scrollDown.SetActive(false);
        }
    }

