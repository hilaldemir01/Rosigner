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


        /*
        float res1,res2;

        res1 = Mathf.Abs(wall1_length - wall_height);
        res2 = Mathf.Abs(wall2_length - wall_height);


        if (wall1_length >= wall2_length)
        {
            if (res1 < 1 )
            {
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
                Light_object.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);

            }
            else
            {
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 2.0f, wall2_length / 2);
                Light_object.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
            }
        }
        else if (wall1_length < wall2_length)
        {
            if (res2 >= 1)
            {
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 2.0f, wall2_length / 2);
                Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 1.5f, wall2_length / 2);
                if (wall2_length < 5)
                {
                    Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 2.5f, wall2_length / 2);
                    Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 2.0f, wall2_length / 2);
                }

            }
            else
            {
                Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
                Light_object.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
            }

        }
        */
        /*

              if (wall1_length>=wall2_length)
              {
                  if (wall1_length < 15)
                  {
                      Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);
                      Light_object.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.5f, wall2_length / 2);

                  }
                  else
                  {
                      Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.10f, wall2_length / 2);
                      Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall1_length * 1.10f, wall2_length / 2);

                  }
              }
              else if (wall1_length < wall2_length)
              {
                  if (wall2_length < 15)
                  {
                      Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 1.5f, wall2_length / 2);
                      Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 1.5f, wall2_length / 2);
                      if (wall2_length < 5)
                      {
                          Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 2.0f, wall2_length / 2);
                          Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 2.0f, wall2_length / 2);
                      }

                  }
                  else
                  {
                      Camera.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 1.25f, wall2_length / 2);
                      Light_object.gameObject.transform.position = new Vector3(wall1_length / 2, wall2_length * 1.25f, wall2_length / 2);
                  }

              }
        */

    }
}
