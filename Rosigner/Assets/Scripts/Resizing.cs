using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Resizing : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    public InputField wall1;
    public InputField wall2;
    public GameObject CanvasWall;
    

    float wall1inp, wall2inp;
    private void Start()
    {
        Show();
    }

    void Update()
    {
        float.TryParse(wall1.text, out float result1);
        wall1inp = result1;

        float.TryParse(wall2.text, out float result2);
        wall2inp = result2;

        // This part of the code is used to set the length and width of the walls.
        wallobj1.gameObject.transform.localScale = new Vector3(wall1inp, 3, 0.2f);
        wallobj2.gameObject.transform.localScale = new Vector3(wall2inp, 3, 0.2f);
        wallobj3.gameObject.transform.localScale = new Vector3(wall1inp, 3, 0.2f);
        wallobj4.gameObject.transform.localScale = new Vector3(wall2inp, 3, 0.2f);

        // This part of the code is used to place walls in a way that they create an enclosed rectangular shape.
        wallobj1.gameObject.transform.localPosition= new Vector3(0, 1.5f, 0);
        wallobj2.gameObject.transform.localPosition= new Vector3(wall1inp + 0.1f, 1.5f, 0.1f);
        wallobj3.gameObject.transform.localPosition= new Vector3(wall1inp, 1.5f, wall2inp + 0.2f);
        wallobj4.gameObject.transform.localPosition= new Vector3(-0.1f, 1.5f, wall2inp + 0.1f);


    }
    // This method is used to close the modal.
    public void Hide()
    {
        CanvasWall.SetActive(false);
    }

    public void Show()
    {
        CanvasWall.SetActive(true);
    }


}
