using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // This method is used to quit from the application.
    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
