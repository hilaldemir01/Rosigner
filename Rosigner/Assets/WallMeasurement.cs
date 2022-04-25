using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallMeasurement : MonoBehaviour
{
    public InputField wall1Height;
    public InputField wall2Height;
    public InputField wallHeight;
    public InputField inputHeightRoomStructure;
    public InputField inputWidthRoomStructure;
    public InputField inputDistanceFromGroundRoomStructure;
    public Button ConfirmButton;
    RosignerContext db = new RosignerContext();

    public void SendToDatabase()
    {
        Room newRoom = new Room();
        newRoom.Wall1Length = float.Parse(wall1Height.text);
        newRoom.Wall2Length = float.Parse(wall2Height.text);
        newRoom.WallHeight = float.Parse(wallHeight.text);

        RoomStructure newRoomStructure = new RoomStructure(); 

        StartCoroutine(db.Room(newRoom));
    }
}
