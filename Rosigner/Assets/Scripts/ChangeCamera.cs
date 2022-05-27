using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject FirstPersonCamera;
    public GameObject MainCamera;
    public GameObject FirstPersonBtn;
    public GameObject QuitText;
    public GameObject Exit;



    void Update()
    {
        //Close the first person mode when press Q
        if (Input.GetKey(KeyCode.Q))
        {
            OpenMainCamera();
        }

    }

    //Open the first person mode
    public void OpenFirstPerson()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonBtn.SetActive(false);
        Exit.SetActive(false);
        MainCamera.SetActive(false);
        FirstPersonCamera.SetActive(true);
        QuitText.SetActive(true);

    }
    //Close the first person mode
    public void OpenMainCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        FirstPersonCamera.SetActive(false);
        QuitText.SetActive(false);
        Exit.SetActive(true);
        FirstPersonBtn.SetActive(true);
        MainCamera.SetActive(true);
    }

   
}
