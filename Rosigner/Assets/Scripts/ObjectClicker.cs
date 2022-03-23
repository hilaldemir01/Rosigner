using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] public GameObject objectToBeSpawned;

    [SerializeField] public Transform parent;
    public GameObject selectedObject;
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


    void SelectObject(GameObject obj)
    {
        if(selectedObject != null)
        {
            if(obj == selectedObject){
                return;
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
