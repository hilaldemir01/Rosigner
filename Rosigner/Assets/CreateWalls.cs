using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWalls : MonoBehaviour
{

    bool creating;
    Camera camera;
    public GameObject start;
    public GameObject end;
    GameObject wall;
    public GameObject wallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        getInput();
    }


    void getInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setStart();
        }else if (Input.GetMouseButton(1))
        {
            setEnd();
        }
        else
        {
            if (creating)
            {
                adjust();
            }
         
        }
    }

    void setStart()
    {
        creating = true;
        start.transform.position = getWorldPoint();
        wall = (GameObject)Instantiate(wallPrefab, start.transform.position, Quaternion.identity);
    }

    void setEnd()
    {
        creating = false;
        end.transform.position = getWorldPoint();
    }
    void adjust()
    {
        end.transform.position = getWorldPoint();
        adjustWall();
    }

    void adjustWall()
    {
        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);
        float ilk =(float)Math.Pow((end.transform.position.x - start.transform.position.x), 2) ;
        float iki =(float)Math.Pow((end.transform.position.z - start.transform.position.z), 2) ;
        float kok = (float)Math.Sqrt(ilk + iki);
        wall.transform.position = start.transform.position + kok / 2 * start.transform.forward;
        wall.transform.rotation = start.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, kok);
    }

    Vector3 getWorldPoint()
    {
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}