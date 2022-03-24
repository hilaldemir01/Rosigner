using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] public GameObject objectToBeSpawned;

    [SerializeField] public Transform parent;
    [SerializeField] public GameObject doorSpawn;
    int distance;
    GameObject selectedObject;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100.0f))
        {
            if(hit.transform != null)
            {
                if(Input.GetMouseButton(0))
                SelectObject(hit.transform.gameObject);
            }
        }
        else
        {
            ClearSelection(); 
        }
    }


    int SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if(obj == selectedObject){
                return 0;
            }

            ClearSelection();
        }
        selectedObject = obj;

        if (selectedObject.tag == "Wall")
        {

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
        }
        else
        {
            return 0;
        }

        distance = 3;
        // This part assigns the position values of the selected wall to the position1
        Vector3 position1 = selectedObject.transform.parent.position;

        // Tag of the parents of the selectedObject is compared, and if one of the walls is clicked and  
        // a distance value is entered, then the door/window will be placed on that wall in the given distance

        if (selectedObject.transform.parent.name == "W1")
        {
            Vector3 position_distance = new Vector3(position1.x + distance , position1.y , position1.z);
            Instantiate(doorSpawn, position_distance, Quaternion.identity, parent);
        }
        else if(selectedObject.transform.parent.name == "W3")
        {
            Vector3 position_distance = new Vector3(position1.x - distance, position1.y, position1.z);
            Instantiate(doorSpawn, position_distance, Quaternion.identity, parent);
        }
        else if (selectedObject.transform.parent.name == "W2" )
        {
            Vector3 position_distance = new Vector3(position1.x , position1.y , position1.z + distance);
            Instantiate(doorSpawn, position_distance, Quaternion.identity, parent);
        }
        else if(selectedObject.transform.parent.name == "W4")
        {
            Vector3 position_distance = new Vector3(position1.x , position1.y , position1.z - distance);
            Instantiate(doorSpawn, position_distance, Quaternion.identity, parent);
        }

        return 0;
    }

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
       

        selectedObject = null;
    }
}
