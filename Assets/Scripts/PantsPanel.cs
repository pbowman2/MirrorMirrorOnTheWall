using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class PantsPanel : MonoBehaviour
{
    public GameObject[] pants;
    public GameObject curser;
    public bool pressed;
    public GameObject scrollUp;
    public GameObject scrollDown;
    public JointType TrackedRight;
    private BodySourceManager bodyManager;
    private Windows.Kinect.Body[] bodies;
    public GameObject BodySrcManager;
    public GameObject spawn;
    int pantsActive;


    bool firstPant = false;
    bool secondPant = false;
    bool thirdPant = false;
    GameObject putOnUser;
    GameObject pressingObject;

    // Set panel
    /* public override void setPanel()
     {
         panel = GameObject.Find("PantPanel");
         stopRightX = 2536f;
         stopLeftX = 3113f;
     }*/


    void Start()
    {
        pressed = false;

        pants[0].SetActive(true);
        pants[1].SetActive(true);
        pants[2].SetActive(true);
        pants[3].SetActive(false);
        pants[4].SetActive(false);
        pants[5].SetActive(false);

        pantsActive = 0;
        pressingObject = GameObject.Find("IsPressed");
    }
    void Update()
    {
        pressingObject = GameObject.Find("IsPressed");
        pressed = pressingObject.GetComponent<Pressing>().handpressed;

        scrollUp.SetActive(true);
        scrollDown.SetActive(true);

        if (pants[0].activeSelf == true)
        {
            scrollUp.SetActive(false);
        }
        else if (pants[3].activeSelf == true)
        {
            pantsActive = 3;
            scrollDown.SetActive(false);

        }

        //Debug.Log("Cursur: " + curser.transform.position.x + " : " + curser.transform.position.y);

        if (curser.transform.position.x < 992 && curser.transform.position.x > 740 && pressed == true)
        {
            pressingObject.GetComponent<Pressing>().handpressed = false;
            pressed = false;
            if (curser.transform.position.y < 1407 && curser.transform.position.y > 1250)
            {
                if (spawn == null || firstPant == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(pants[pantsActive]); //cloning
                    spawn.AddComponent<Joints>();
                    firstPant = true;
                    secondPant = false;
                    thirdPant = false;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                }

            }
            else if (curser.transform.position.y < 1205 && curser.transform.position.y > 1050)
            {
                if (spawn == null || secondPant == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(pants[pantsActive + 1]);
                    spawn.AddComponent<Joints>();
                    firstPant = false;
                    secondPant = true;
                    thirdPant = false;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                    Debug.Log("2: ");
                }

            }
            else if (curser.transform.position.y < 1000 && curser.transform.position.y > 884)
            {
                if (spawn == null || thirdPant == false)
                {
                    Destroy(spawn);
                    spawn = null;
                    spawn = (GameObject)Instantiate(pants[pantsActive + 2]);
                    spawn.AddComponent<Joints>();
                    firstPant = false;
                    secondPant = false;
                    thirdPant = true;
                    spawn.transform.position = new Vector3(0, 20.4f, 92.9f);
                    Debug.Log("3: ");

                }
            }
        }



    }

    public void PressUpButton()
    {
        pants[0].SetActive(true);
        pants[1].SetActive(true);
        pants[2].SetActive(true);
        pants[3].SetActive(false);
        pants[4].SetActive(false);
        pants[5].SetActive(false);

        scrollUp.SetActive(false);
        scrollDown.SetActive(true);
    

    }

    public void PressDownButton()
    {
        pantsActive += 3;

        pants[0].SetActive(false);
        pants[1].SetActive(false);
        pants[2].SetActive(false);
        pants[3].SetActive(true);
        pants[4].SetActive(true);
        pants[5].SetActive(true);
        scrollUp.SetActive(true);
        scrollDown.SetActive(false);
    }
}
