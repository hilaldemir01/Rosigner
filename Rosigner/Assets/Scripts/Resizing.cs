using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Resizing : MonoBehaviour
{
    public GameObject wallobj1;
    public GameObject wallobj2;
    public GameObject wallobj3;
    public GameObject wallobj4;
    public GameObject floor;
    public InputField wall1;
    public InputField wall2;
    public InputField height;
    public GameObject CanvasWall;
    public Text ErrorMessage;
    RosignerContext db = new RosignerContext();
    int RoomID;
    List<Wall> wallList = new List<Wall>();

    float wall1inp, wall2inp, heightinp;
    Room newRoom = new Room(); 


    private void Start()
    {
        Show();
    }

    void Update()
    {

        CreatingWalls();
    }
    // This method is used to close the modal.
    public void Hide()
    {
        CanvasWall.SetActive(false);
    }

    public void Show()
    {
        CanvasWall.SetActive(true);
    }

    bool CreatingWalls()
    {

        float.TryParse(wall1.text, out float result1);
        wall1inp = result1;

        float.TryParse(wall2.text, out float result2);
        wall2inp = result2;

        float.TryParse(height.text, out float result3);
        heightinp = result3;

        if (wall1inp <= 0 || wall2inp<=0 || heightinp <=0)
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter non positive value";
            return false;

        } else if (wall1inp > 10 || wall2inp > 10 || heightinp > 10 )
        {
            ErrorMessage.gameObject.SetActive(true);
            ErrorMessage.text = "Please do not enter value more than 10 meters";
            return false;
        }
        else
        {
             // This part of the code is used to set the length and width of the walls.
            wallobj1.gameObject.transform.localScale = new Vector3(wall1inp, heightinp, 0.2f);
            wallobj2.gameObject.transform.localScale = new Vector3(wall2inp, heightinp, 0.2f);
            wallobj3.gameObject.transform.localScale = new Vector3(wall1inp, heightinp, 0.2f);
            wallobj4.gameObject.transform.localScale = new Vector3(wall2inp, heightinp, 0.2f);
            floor.gameObject.transform.localScale = new Vector3(wall1inp, 0.1f, wall2inp);


            // This part of the code is used to place walls in a way that they create an enclosed rectangular shape.
            wallobj1.gameObject.transform.position = new Vector3(0, 0, 0);
            wallobj2.gameObject.transform.position = new Vector3(wall1inp + 0.1f, 0, 0.1f);
            wallobj3.gameObject.transform.position = new Vector3(wall1inp, 0, wall2inp + 0.2f);
            wallobj4.gameObject.transform.position = new Vector3(-0.1f, 0, wall2inp + 0.1f);
            floor.gameObject.transform.position = new Vector3(wall1inp/2.0f, -0.05f, (wall2inp/2.0f)+0.1f);

            ErrorMessage.gameObject.SetActive(false);
            return true;
        }

    }

    public void ConfirmButton()
    {
        bool hasEroors=CreatingWalls();
        if (hasEroors == true) // if there are no errors
        {
            StartCoroutine(db.Room(LoginSystem.instance.loggedinUser.UserId, getRoomID)); // generate a new room id and insert those to the db


            Hide();
        }
    }

    public void getRoomID(string roomID)
    {
        //RoomID = Convert.ToInt32(roomID); 
        newRoom.UserID = LoginSystem.instance.loggedinUser.UserId;
        newRoom.RoomID = int.Parse(roomID);

        wallList.Add(new Wall() { WallName = "W1", WallLength = wall1inp, WallHeight = heightinp, RoomID = newRoom.RoomID });
        wallList.Add(new Wall() { WallName = "W2", WallLength = wall2inp, WallHeight = heightinp, RoomID = newRoom.RoomID });
        wallList.Add(new Wall() { WallName = "W3", WallLength = wall1inp, WallHeight = heightinp, RoomID = newRoom.RoomID });
        wallList.Add(new Wall() { WallName = "W4", WallLength = wall2inp, WallHeight = heightinp, RoomID = newRoom.RoomID });

        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(db.Wall(wallList[i], getWallID)); // generate new wall ids and insert those to the db
        }

    }
    public void getWallID(string WallID)
    {
        //Debug.Log("WallID:" + WallID);
    }

}
