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
    public InputField inputDistanceFromRedDot;
    public Button ConfirmButton;
    RosignerContext db = new RosignerContext();

    public void callSendToDatabase()
    {
        StartCoroutine(SendToDatabase());
    }
    IEnumerator SendToDatabase()
    {
        Room newRoom = new Room();
        newRoom.Wall1Length = float.Parse(wall1Height.text);
        newRoom.Wall2Length = float.Parse(wall2Height.text);
        newRoom.WallHeight = float.Parse(wallHeight.text);

        RoomStructure newRoomStructure = new RoomStructure();
        newRoomStructure.DistanceFromRedDot = float.Parse(inputDistanceFromRedDot.text);
        newRoomStructure.DistanceFromGround = float.Parse(inputDistanceFromGroundRoomStructure.text);
        newRoomStructure.RoomStructureHeight = float.Parse(inputHeightRoomStructure.text);
        newRoomStructure.RoomStructureWidth = float.Parse(inputWidthRoomStructure.text);


        StartCoroutine(db.Room(newRoom));
        yield return new WaitForSeconds(1);

    }
}
