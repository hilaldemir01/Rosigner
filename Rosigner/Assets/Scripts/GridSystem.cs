using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;


public class GridSystem : MonoBehaviour
{
    List<Wall> wallList = new List<Wall>();
    RosignerContext db = new RosignerContext();
    [SerializeField] private Transform testTransform; //temizlenebilir
    private GridXZ<GridObject> grid;
    public int wallX,wallY;
    public Wall wall;
    public int a,b;


/*
      public void fetchWallInformation(List<Wall> newWall)
    {
        for(int i = 0; i < newWall.Count; i++)
        {
            wallList.Add(new Wall() { WallID = newWall[i].WallID, WallName = newWall[i].WallName, WallLength = newWall[i].WallLength, WallHeight = newWall[i].WallHeight, RoomID = newWall[i].RoomID });
        }
         GettingWallsInfo();
    }
    
   void GettingWallsInfo()
    {
        // This part of the code is used to set the length and width of the walls.
        float a =(wallList[0].WallLength);
        float b=(wallList[0].WallHeight);
        wallX=(int)a;
        wallY=(int)b;

    }*/
    
    
    // Start is called before the first frame update
   private void Awake() {
       string[] allWalls = { "W1", "W2", "W3", "W4" };
       //StartCoroutine(db.WallInformation(allWalls, fetchWallInformation));

        int gridWidth = 600;
        int gridHeight = 300;
        float cellSize = 0.01f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
        //placedObjectTypeSO = null;// placedObjectTypeSOList[0];
    }

    // Update is called once per frame
public class GridObject { //modelde yapılabilir

        private GridXZ<GridObject> grid;
        private int x, y;
       
        public Transform transform;  //private Transform transform;

        public GridObject(GridXZ<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            //placedObject = null;
        }
        
          public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearTransform() {
            transform = null;
            grid.TriggerGridObjectChanged(x, y);
        }
          public bool CanBuild() {
            return transform == null;
        }

         public override string ToString() {
            return x + ", " + y+"\n" + transform ;
        }
    }
    /*private void Update(){
        if (Input.GetMouseButtonDown(0)){
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(),out int x, out int z);
            GridObject gridObject=grid.GetGridObject(x,z);
            if(gridObject.CanBuild()){
                Transform builtTransform=Instantiate(testTransform , grid.GetWorldPosition(x,z),Quaternion.identity);
                gridObject.SetTransform(builtTransform);
                
            }
        }
    }*/
}
