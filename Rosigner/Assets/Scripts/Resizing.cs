using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizing : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    public float wall1;
    public float wall2;
 
    // Start is called before the first frame update
    void Start()
    {
        Vector3 local = transform.localScale;
        Vector3 local2 = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        wallobj1.gameObject.transform.localScale =  new Vector3(wall1, 3, 0.2f);

        wallobj2.gameObject.transform.localScale = new Vector3(wall2, 3, 0.2f);
        wallobj3.gameObject.transform.localScale = new Vector3(wall1, 3, 0.2f);
        wallobj4.gameObject.transform.localScale = new Vector3(wall2, 3, 0.2f);


        wallobj1.gameObject.transform.localPosition= new Vector3(0, 1.5f, 0);
        wallobj2.gameObject.transform.localPosition= new Vector3(wall1+0.1f, 1.5f, 0.1f);
        wallobj3.gameObject.transform.localPosition= new Vector3(wall1, 1.5f, wall2+0.2f);
        wallobj4.gameObject.transform.localPosition= new Vector3(-0.1f, 1.5f, wall2+0.1f);


    }
}
