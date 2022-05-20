using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static string PreviousLevel { get; set; }
    private void OnDestroy()
    {
        PreviousLevel = gameObject.scene.name;
    }
}
