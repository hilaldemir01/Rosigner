using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallDefiner : MonoBehaviour
{
    [SerializeField] public GameObject objectToBeSpawned;

    [SerializeField] public Transform parent;
    [SerializeField] public GameObject doorSpawn;
    int distance;
    GameObject selectedObject;
    GameObject tempObject;
    public GameObject CanvasDistance;
    public GameObject PanelWindowChosing;
    public InputField inputDistinceFromWall;
    public Dropdown DropdownRoomStructure;
    public InputField inputHeight;
    public InputField inputWidth;
    public InputField inputDistanceFromGround;

    float height, width;
    // Update is called once per frame
    void Update()
    {


        // This method is use to select the clicked wall. 

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100.0f) && CanvasDistance.activeSelf == false)
        {
            if(hit.transform != null)
            {
                if(Input.GetMouseButton(0))
                SelectObject(hit.transform.gameObject);
                
            }
        }

        if (DropdownRoomStructure.GetComponent<Dropdown>().value == 2)
        {
            PanelWindowChosing.SetActive(true);
        }
        else
        {
            PanelWindowChosing.SetActive(false);
        }
        /*
        else
        {
            if(!CanvasDistance.activeSelf)
            {
            // If other place rather than the clicked wall, the highlight will be removed.
                 ClearSelection();
           
            }
            
        }
        */
    }

    // This method is used to highlight the clicked wall and also a red dot is placed on the wall.
    int SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if(obj == selectedObject){
                return 0;
            }

            if (!CanvasDistance.activeSelf)
            {
                // If other place rather than the clicked wall, the highlight will be removed.
                ClearSelection();

            }
            else { 
            }
        }
        selectedObject = obj;

        if (selectedObject.tag == "Wall")
        {
            // position of the red dot is defined here:

            Vector3 position = selectedObject.transform.parent.position;
            Vector3 startposition = new Vector3(position.x, 3.1f, position.z); 
            Instantiate(objectToBeSpawned, startposition, Quaternion.identity, parent);

            Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.gray;
                r.material = m;
            }
            tempObject = selectedObject;
            CanvasDistance.SetActive(true);


          
   
        }
        else
        {
            return 0;
        }

        // To be able to place doors or windows, we use the following method
        
        return 0;
    }

    // If another area is clicked, then the highlight is removed, and also red dot is removed as well.
    void ClearSelection()
    {
        if(selectedObject == null)
        {
            return;
        }
        if (selectedObject.tag == "Wall"){
            Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.white;
                r.material = m;
            }
            Destroy(GameObject.Find("RedDot(Clone)"));
           
        }
       
        CanvasDistance.SetActive(false);
     
    }

    int RoomStructures()
    {
        float wallDistance, groundDistance;

        float.TryParse(inputDistinceFromWall.text, out float result);
        wallDistance = result;

        float.TryParse(inputDistanceFromGround.text, out float result2);
        groundDistance = result2;


        // This part assigns the position values of the selected wall to the position1
        Vector3 position1 = selectedObject.transform.parent.position;

        // Tag of the parents of the selectedObject is compared, and if one of the walls is clicked and  
        // a distance value is entered, then the door/window will be placed on that wall in the given distance

        if (selectedObject.transform.parent.name == "W1")
        {
            Vector3 position_distance = new Vector3(position1.x + wallDistance, groundDistance, position1.z);
            RoomStructureSizing();
            Instantiate(doorSpawn, position_distance, Quaternion.Euler(new Vector3(0, 0, 0)), parent);
        }
        else if (selectedObject.transform.parent.name == "W2")
        {
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z + wallDistance);
            RoomStructureSizing();
            Instantiate(doorSpawn, position_distance, Quaternion.Euler(new Vector3(0, 270, 0)), parent);
        }
        else if (selectedObject.transform.parent.name == "W3")
        {
            Vector3 position_distance = new Vector3(position1.x - wallDistance, groundDistance, position1.z);
            RoomStructureSizing();
            Instantiate(doorSpawn, position_distance, Quaternion.Euler(new Vector3(0, 180, 0)), parent);
       
            
        }
        else if (selectedObject.transform.parent.name == "W4")
        {
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z - wallDistance);
            RoomStructureSizing();
            Instantiate(doorSpawn, position_distance, Quaternion.Euler(new Vector3(0, 90, 0)), parent);
            
        }
        return 0;
    }

    void RoomStructureSizing()
    {

        float.TryParse(inputHeight.text, out float result1);
        height = result1;


        float.TryParse(inputWidth.text, out float result2);
        width = result2;

        doorSpawn.transform.localScale= new Vector3(width, height, 0.5f);
    }


    public void confirm()
    {
        selectedObject=tempObject;
        ClearSelection();
        RoomStructures();
        selectedObject = null;

    }

}




