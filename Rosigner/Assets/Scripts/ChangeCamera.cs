using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject FirstPersonCamera;
    public GameObject MainCamera;
   // GameObject FirstPersonCanvas;
   // GameObject MainCameraCanvas;


    public void OpenFirstPerson()
    {
       // MainCameraCanvas.SetActive(false);
        MainCamera.SetActive(false);
        FirstPersonCamera.SetActive(true);
       // FirstPersonCanvas.SetActive(true);

    }

    public void OpenMainCamera()
    {
        FirstPersonCamera.SetActive(false);
       // FirstPersonCanvas.SetActive(false);
       // MainCameraCanvas.SetActive(true);
        MainCamera.SetActive(true);
    }



}
