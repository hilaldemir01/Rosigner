using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizing : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    public float wall1;
    public float wall2;
    public Canvas canvas1;

    void Update()
    {
        // This part of the code is used to set the length and width of the walls.
        wallobj1.gameObject.transform.localScale =  new Vector3(wall1, 3, 0.2f);

        wallobj2.gameObject.transform.localScale = new Vector3(wall2, 3, 0.2f);
        wallobj3.gameObject.transform.localScale = new Vector3(wall1, 3, 0.2f);
        wallobj4.gameObject.transform.localScale = new Vector3(wall2, 3, 0.2f);

        // This part of the code is used to place walls in a way that they create an enclosed rectangular shape.
        wallobj1.gameObject.transform.localPosition= new Vector3(0, 1.5f, 0);
        wallobj2.gameObject.transform.localPosition= new Vector3(wall1+0.1f, 1.5f, 0.1f);
        wallobj3.gameObject.transform.localPosition= new Vector3(wall1, 1.5f, wall2+0.2f);
        wallobj4.gameObject.transform.localPosition= new Vector3(-0.1f, 1.5f, wall2+0.1f);


    }
    // This method is used to close the modal.
    public void Confirm()
    {
        canvas1.enabled = false;
    }
}
