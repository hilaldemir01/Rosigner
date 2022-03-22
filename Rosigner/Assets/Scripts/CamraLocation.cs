using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraLocation : MonoBehaviour
{
    public GameObject Camera;
    
    public GameObject Wall1_location;
    public GameObject Wall2_location;


    void Start()
    {
        Vector3 local = transform.localScale;
        Vector3 local2 = transform.localPosition;
    }


    void Update()
    {
        float location1, location2, wall1_length, wall2_length;

        wall1_length = Wall1_location.gameObject.transform.localScale.x;
        wall2_length = Wall2_location.gameObject.transform.localScale.x;


        Camera.gameObject.transform.localPosition = new Vector3(wall1_length/2 ,20, wall2_length/2);
       

    }




}
