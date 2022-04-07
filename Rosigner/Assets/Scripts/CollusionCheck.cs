using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollusionCheck : MonoBehaviour
{

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "windowtry")
        {
            Debug.Log("kesisistiler.");

        }
    }
}
