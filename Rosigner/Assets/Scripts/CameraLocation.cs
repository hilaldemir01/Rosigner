using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocation : MonoBehaviour
{
    public GameObject Camera;
    
    public GameObject Wall1_location;
    public GameObject Wall2_location;

    void Update()
    {
        float wall1_length, wall2_length;

        // This part of the code is used to get the length of the walls.

        wall1_length = Wall1_location.gameObject.transform.localScale.x;
        wall2_length = Wall2_location.gameObject.transform.localScale.x;

        // This part of the code is used to fix the distance between the floor and camera. Also, the camera is focused on the center of the room

        if (wall1_length>=wall2_length)
        {
            if (wall1_length < 15)
            {
                Camera.gameObject.transform.localPosition = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
            }
            else
            {
                Camera.gameObject.transform.localPosition = new Vector3(wall1_length / 2, wall1_length * 1.10f, wall2_length / 2);
            }
        }
        else if (wall1_length < wall2_length)
        {
            if (wall2_length < 15)
            {
                Camera.gameObject.transform.localPosition = new Vector3(wall1_length / 2, wall2_length * 1.5f, wall2_length / 2);
                if (wall2_length < 5)
                {
                    Camera.gameObject.transform.localPosition = new Vector3(wall1_length / 2, wall2_length * 2.0f, wall2_length / 2);
                }

            }
            else
            {
                Camera.gameObject.transform.localPosition = new Vector3(wall1_length / 2, wall2_length * 1.25f, wall2_length / 2);
            }

        }


    }
}
