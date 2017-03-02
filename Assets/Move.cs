using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float max;
    public float min;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < max)
        {
            transform.position += Vector3.right * Time.deltaTime * 400f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > min)
        {
            transform.position += Vector3.left * Time.deltaTime * 400f;
        }
    }
}
