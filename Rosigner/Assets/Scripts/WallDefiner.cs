using UnityEngine;
using UnityEngine.UI;

public class WallDefiner : MonoBehaviour
{
    [SerializeField] public GameObject objectToBeSpawned;

    [SerializeField] public Transform parent;
    [SerializeField] public GameObject doorSpawn;
    [SerializeField] public GameObject windowSpawn;
    int distance;
    GameObject selectedObject;
    GameObject tempObject;
    GameObject tempAsset;
    public GameObject CanvasDistance;
    public GameObject PanelWindowChosing;
    public InputField inputDistanceFromWall;
    public Dropdown DropdownRoomStructure;
    public InputField inputHeight;
    public InputField inputWidth;
    public InputField inputDistanceFromGround;
    public GameObject CanvasWall;
    public Button ConfirmButton;
    public Text ErrorMessage;

    private int isTriggered=1;




    float height, width;


    // Update is called once per frame
    void Update()
    {


        // This method is use to select the clicked wall. 

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100.0f) && CanvasDistance.activeSelf == false && CanvasWall.activeSelf == false)
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
            float reddot_height = selectedObject.transform.parent.localScale.y + 0.1f;
            Vector3 position = selectedObject.transform.parent.position;
            Vector3 startposition = new Vector3(position.x, reddot_height, position.z); 
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
        Color c = new Color(0.9339623f, 0.8399786f, 0.7084016f, 1);
        if (selectedObject == null)
        {
            return;
        }
        if (selectedObject.tag == "Wall"){
            Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = c;
                r.material = m;
            }
            Destroy(GameObject.Find("RedDot(Clone)"));
           
        }
        

        CanvasDistance.SetActive(false);
     
    }

    bool RoomStructures()
    {
        float wallDistance, groundDistance, TempGroundDistance;
        float tempScale;

        float.TryParse(inputDistanceFromWall.text, out float result);
        wallDistance = result;

        float.TryParse(inputDistanceFromGround.text, out float result2);
        TempGroundDistance = result2;

        // This part assigns the position values of the selected wall to the position1
        Vector3 position1 = selectedObject.transform.parent.position;

        // Tag of the parents of the selectedObject is compared, and if one of the walls is clicked and  
        // a distance value is entered, then the door/window will be placed on that wall in the given distance


        if (DropdownRoomStructure.GetComponent<Dropdown>().value == 2)//window
        {

            groundDistance= TempGroundDistance;
            tempAsset = windowSpawn;
        }
        else
        {
            groundDistance = 0;
            tempAsset = doorSpawn;
        }

        if (selectedObject.transform.parent.name == "W1")
        {
            tempScale=selectedObject.transform.parent.localScale.x;
            Vector3 position_distance = new Vector3(position1.x + wallDistance, groundDistance, position1.z);
            if(RoomStructureSizing(tempScale, wallDistance, groundDistance) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 0, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w1");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W2")
        {
            tempScale = selectedObject.transform.parent.localScale.x;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z + wallDistance);

            if (RoomStructureSizing(tempScale, wallDistance, groundDistance) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 270, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w2");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W3")
        {
            tempScale = selectedObject.transform.parent.localScale.x;
            Vector3 position_distance = new Vector3(position1.x - wallDistance, groundDistance, position1.z);
            if (RoomStructureSizing(tempScale, wallDistance, groundDistance) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w3");
                return false;
            }
         
    
        }
        else if (selectedObject.transform.parent.name == "W4")
        {
            tempScale = selectedObject.transform.parent.localScale.x;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z - wallDistance);
           
            if(RoomStructureSizing(tempScale, wallDistance, groundDistance) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 90, 0)), parent);
            }
            else
            {
                Debug.Log("Asti Boyu w4");
                return false;
            }

            
            
        }
        return true;
    }

    bool RoomStructureSizing(float tempScale, float wallDistance,float groundDistance)
    {

        
        
        float.TryParse(inputHeight.text, out float result1);
        height = result1;

        float.TryParse(inputWidth.text, out float result2);
        width = result2;


        if(wallDistance<0 || groundDistance < 0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter negative value";
            return false;
        }
        else if (width<=0 || height<=0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter non positive value for width and height";
            return false;
        }
        else if (width + wallDistance > tempScale)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter a width value more than the Wall width";
            return false;
        }
        else if(height+groundDistance > 3)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter a height value more than the Wall height";
            return false;
        }else if(isTriggered == 0)
        {
            ErrorMessage.gameObject.SetActive(true);  // window and door collider.
            ErrorMessage.text = "The room structure to be places is colliding with another one";
            return false;
            
        }
        else
        {
            tempAsset.transform.localScale= new Vector3(width, height, 0.3f);
            ErrorMessage.gameObject.SetActive(false);
            return true;
        }

        
    }

    public void SetisTrigger(int x)
     {
        isTriggered =  x;
     }




public void confirm()
    {

        selectedObject=tempObject;

        

        bool hasEroors = RoomStructures();
        if(hasEroors== true)
        {
            ClearSelection();
        }
      
        selectedObject = null;


    }


}




