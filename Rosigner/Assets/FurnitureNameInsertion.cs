using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureNameInsertion : MonoBehaviour
{
    RosignerContext db = new RosignerContext();
    // This class is to add furniture names to the table if the table is empty
    void Start()
    {
        StartCoroutine(db.furnitureNameInsertion());
    }
}
