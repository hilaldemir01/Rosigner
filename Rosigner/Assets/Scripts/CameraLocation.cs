using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocation : MonoBehaviour
{
    public GameObject Camera;
    
    public GameObject Wall1_location;
    public GameObject Wall2_location;
    public GameObject Light_object;

    void Update()
    {
        float wall1_length, wall2_length, wall_height;

        // This part of the code is used to get the length of the walls.

        wall1_length = Wall1_location.gameObject.transform.localScale.x;
        wall2_length = Wall2_location.gameObject.transform.localScale.x;
        wall_height = Wall1_location.gameObject.transform.localScale.y;     // Set Camre to height of walls ****

        // This part of the code is used to fix the distance between the floor and camera. Also, the camera is focused on the center of the room


        if (wall1_length >= wall2_length)
        {

                float camposition1= (wall1_length / 2) * Mathf.Sqrt(5) + wall_height;
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, camposition1, (wall2_length / 2)+0.1f);
                Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, camposition1, wall2_length / 2);

            
        }
        else 
        {
                float camposition2 = (wall2_length / 2) * Mathf.Sqrt(5) + wall_height;
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, camposition2, (wall2_length / 2) + 0.1f);
                Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, camposition2, wall2_length / 2);
  
           
        }


    }
}
