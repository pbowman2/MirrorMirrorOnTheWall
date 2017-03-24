using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shirt : MonoBehaviour {
    public GameObject curser;

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.name == "Cursor")
        {
            //col.Add(col.collider); // saves the enemy for later respawn
            col.collider.gameObject.SetActive(false); // deactivate instead of destroy so you could later reactivate (respawn) him
            Debug.Log("Yikes");
        }
        Debug.Log("Nope");
    }

}
