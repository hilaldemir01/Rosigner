using Assets.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallDefiner : MonoBehaviour
{
    [SerializeField] public GameObject objectToBeSpawned;

    [SerializeField] public Transform parent;
    [SerializeField] public GameObject doorSpawn;
    [SerializeField] public GameObject windowSpawn;
    int distance;
    GameObject selectedObject, tempObject, tempAsset;

    public GameObject CanvasDistance, PanelWindowChosing, CanvasGoAddFurniture, CanvasWall;
    
    public InputField inputDistanceFromWall;
    public Dropdown DropdownRoomStructure;
    public InputField inputHeight, inputWidth, inputDistanceFromGround;

    public Button ConfirmButton;
    public Text ErrorMessage;
    public static WallDefiner instance;
    RosignerContext db = new RosignerContext();
    private int isTriggered=1;
    RoomStructure newRoomStructure = new RoomStructure();
    RoomStructureLocation newRoomStructureLocation = new RoomStructureLocation();
    float height, width;
    RegisteredUser loggedinUser = new RegisteredUser();
    Level leve;

    private void Start()
    {

        Debug.Log(Level.PreviousLevel);
        if (Level.PreviousLevel == "Login")
        { 
            loggedinUser = LoginSystem.instance.loggedinUser;
            Debug.Log(loggedinUser.FirstName + loggedinUser.LastName);
            Debug.Log(loggedinUser.UserId);
        }

    }

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

        checkforGoAddFurniture();

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
            float reddot_height = selectedObject.transform.parent.localScale.y + 0.01f;
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
        float tempScaleWidth, tempScaleHeight;

        float.TryParse(inputDistanceFromWall.text, out float result);
        wallDistance = result/ 100.0f;

        float.TryParse(inputDistanceFromGround.text, out float result2);
        TempGroundDistance = result2/ 100.0f;

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
            tempScaleWidth=selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x + wallDistance, groundDistance, position1.z);
            if(RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 0, 0)), parent);
                newRoomStructureLocation.LocationX = position_distance.x;
                newRoomStructureLocation.LocationY = position_distance.y;
                newRoomStructureLocation.LocationZ = position_distance.z;
                newRoomStructureLocation.RotationX = 0;
                newRoomStructureLocation.RotationY = 0;
                newRoomStructureLocation.RotationZ = 0;

                StartCoroutine(db.RoomStructure(selectedObject.transform.parent.name, tempAsset.name.ToString(), newRoomStructure, newRoomStructureLocation));
            }
            else
            {
                Debug.Log("Asti Boyu w1");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W2")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z + wallDistance);

            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 270, 0)), parent);
                newRoomStructureLocation.LocationX = position_distance.x;
                newRoomStructureLocation.LocationY = position_distance.y;
                newRoomStructureLocation.LocationZ = position_distance.z;
                newRoomStructureLocation.RotationX = 0;
                newRoomStructureLocation.RotationY = 270;
                newRoomStructureLocation.RotationZ = 0;

                StartCoroutine(db.RoomStructure(selectedObject.transform.parent.name, tempAsset.name.ToString(), newRoomStructure, newRoomStructureLocation));
            }
            else
            {
                Debug.Log("Asti Boyu w2");
                return false;
            }
        }
        else if (selectedObject.transform.parent.name == "W3")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x - wallDistance, groundDistance, position1.z);
            if (RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 180, 0)), parent);
                newRoomStructureLocation.LocationX = position_distance.x;
                newRoomStructureLocation.LocationY = position_distance.y;
                newRoomStructureLocation.LocationZ = position_distance.z;
                newRoomStructureLocation.RotationX = 0;
                newRoomStructureLocation.RotationY = 180;
                newRoomStructureLocation.RotationZ = 0;

                StartCoroutine(db.RoomStructure(selectedObject.transform.parent.name, tempAsset.name.ToString(), newRoomStructure, newRoomStructureLocation));
            }
            else
            {
                Debug.Log("Asti Boyu w3");
                return false;
            }
         
    
        }
        else if (selectedObject.transform.parent.name == "W4")
        {
            tempScaleWidth = selectedObject.transform.parent.localScale.x;
            tempScaleHeight = selectedObject.transform.parent.localScale.y;
            Vector3 position_distance = new Vector3(position1.x, groundDistance, position1.z - wallDistance);
           
            if(RoomStructureSizing(tempScaleHeight, tempScaleWidth, wallDistance, groundDistance, selectedObject.transform.parent.name) == true)
            {
                Instantiate(tempAsset, position_distance, Quaternion.Euler(new Vector3(0, 90, 0)), parent);
                newRoomStructureLocation.LocationX = position_distance.x;
                newRoomStructureLocation.LocationY = position_distance.y;
                newRoomStructureLocation.LocationZ = position_distance.z;
                newRoomStructureLocation.RotationX = 0;
                newRoomStructureLocation.RotationY = 90;
                newRoomStructureLocation.RotationZ = 0;

                StartCoroutine(db.RoomStructure(selectedObject.transform.parent.name, tempAsset.name.ToString(), newRoomStructure, newRoomStructureLocation));
            }
            else
            {
                Debug.Log("Asti Boyu w4");
                return false;
            }
        }
        return true;
    }

    bool RoomStructureSizing(float tempScaleHeight, float tempScaleWidth, float wallDistance,float groundDistance, string wallName)
    {
        float.TryParse(inputHeight.text, out float result1);
        height = result1/ 100.0f;

        float.TryParse(inputWidth.text, out float result2);
        width = result2/100.0f;

        if (DropdownRoomStructure.GetComponent<Dropdown>().value == 0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please select the room structure type";
            return false;
        }
        else if (DropdownRoomStructure.GetComponent<Dropdown>().value == 2 && inputDistanceFromGround.text == "")
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please fill out all blanks";
            return false;
        }
        else if(wallDistance<0 || groundDistance < 0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter negative value";
            return false;
        }else if(inputDistanceFromWall.text == "")
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please fill out all blanks";
            return false;
        }
        else if (width<=0 || height<=0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter non positive value for width and height";
            return false;
        }
        else if (width + wallDistance > tempScaleWidth)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter a width value more than the Wall width";
            return false;
        }
        else if(height+groundDistance > tempScaleHeight)
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
            newRoomStructure.StrructureLength = height;
            newRoomStructure.StrructureWidth = width;
            newRoomStructure.RedDotDistance = wallDistance;
            newRoomStructure.GroundDistance = groundDistance;

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
            inputDistanceFromGround.text = "";
            inputDistanceFromWall.text = "";
            inputHeight.text = null;
            inputWidth.text = null;
            DropdownRoomStructure.value = 0;
        
        }
       
        selectedObject = null;


    }

    public void CancelButton()
    {

        selectedObject = tempObject;

        Color c = new Color(0.9339623f, 0.8399786f, 0.7084016f, 1);
        if (selectedObject == null)
        {
            return;
        }
        if (selectedObject.tag == "Wall")
        {
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

        inputDistanceFromGround.text = "";
        inputDistanceFromWall.text = "";
        inputHeight.text = null;
        inputWidth.text = null;
        DropdownRoomStructure.value = 0;

        selectedObject = null;

    }

    public void checkforGoAddFurniture()
    {
        if (!CanvasDistance.activeSelf && !CanvasWall.activeSelf)
        {
            CanvasGoAddFurniture.SetActive(true);
            
        }
        else
        {
            CanvasGoAddFurniture.SetActive(false);
           
        }
    }


  
    public void backButton()
    {
        if (loggedinUser.Email != null)
        {
            SceneManager.LoadScene("PreviousDesigns");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

}