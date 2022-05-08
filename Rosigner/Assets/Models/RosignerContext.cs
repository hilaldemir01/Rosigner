using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Models
{
    class RosignerContext
    {

        public RegisteredUser instance;
        public RegisteredUser currentUser = new RegisteredUser();
        public int RoomID;
        List<Wall> wallList = new List<Wall>();

        #region Register
        public IEnumerator Register(RegisteredUser newUser)
        {
            Text notificationTxt = GameObject.Find("Canvas/notification").GetComponent<Text>();


            WWWForm form = new WWWForm();
            form.AddField("unity", "register");
            form.AddField("firstname", newUser.FirstName);
            form.AddField("lastname", newUser.LastName);
            form.AddField("gender", newUser.Gender);
            form.AddField("email", newUser.Email);
            form.AddField("password", newUser.Hash);

            string message = "";


            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userRegister.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    message = "" + www.error;
                    notificationTxt.gameObject.SetActive(true);
                    notificationTxt.text = message;
                }
                else
                {
                    // if there are no errors then user account is created:

                    if (www.downloadHandler.text.Contains("Registration is successful"))
                    {
                        message = "" + www.downloadHandler.text;


                        notificationTxt.gameObject.SetActive(true);
                        notificationTxt.text = message;
                        yield return new WaitForSeconds(1);
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

                    }
                    else
                    {
                        message = "" + www.downloadHandler.text;
                        notificationTxt.gameObject.SetActive(true);
                        notificationTxt.text = message;
                    }
                }
            }

            yield return new WaitForSeconds(1);

        }
        #endregion

        #region Login
        public IEnumerator Login(string email, string password, System.Action<string> callback)
        {
            //Text notificationTxt2 = GameObject.Find("Login/Canvas/notification").GetComponent<Text>();

            WWWForm form = new WWWForm();
            form.AddField("unity", "login");
            form.AddField("email", email);
            form.AddField("password", password);




            // database connection is done here:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userLogin.php", form))
            {
                // checking if there are any database connection errors:

                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    // notificationTxt2.gameObject.SetActive(true);
                    //  notificationTxt2.text = "" + www.error;
                    callback("" + www.error);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

                }
                else
                {
                    // if users credentials find a match in the database, then users can log in to their accounts 

                    if (www.downloadHandler.text.Contains("Login success!"))
                    {
                        callback(""+www.downloadHandler.text);
                        yield return new WaitForSeconds(1);

                    }
                    else
                    {
                        //      notificationTxt2.gameObject.SetActive(true);
                        //     notificationTxt2.text = "" + www.downloadHandler.text;
                        callback("" + www.downloadHandler.text);
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

                        //Debug.Log(www.downloadHandler.text);
                    }
                }
            }
        }
        #endregion

        #region Fetch User Information
        // https://www.youtube.com/watch?v=5jdGmGcmyT4&list=PLUGBd0sVm3sjrdmBd6kCIKRtJN2tZuQcb&index=20&ab_channel=GinfiSoftware
        public IEnumerator LoginUserInfo(string email, RegisteredUser loggedinUser, System.Action<RegisteredUser> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "loginuserinfo");
            form.AddField("email", email);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/loginuserinfo.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // storing the fetched user credentials 
                    string returnedUser = www.downloadHandler.text;

                    // splitting the returned string according to the class attributes : https://csharp-tutorials.com/tr-TR/linq/Split
                    string[] userArray = returnedUser.Split(';');

                    // creating a registereduser object in order to store user credentials
                    loggedinUser.UserId = int.Parse(userArray[0]);
                    loggedinUser.FirstName = userArray[1];
                    loggedinUser.LastName = userArray[2];
                    loggedinUser.Gender = int.Parse(userArray[3]);
                    loggedinUser.Email = email;

                    //checking if the returned values are correct
                    Debug.Log(loggedinUser.FirstName);
                    Debug.Log(loggedinUser.LastName);
                    Debug.Log(loggedinUser.Gender);
                    Debug.Log(loggedinUser.Email);

                    currentUser = loggedinUser;
                    callback(loggedinUser);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("PreviousDesigns");

                }
            }


        }
        #endregion

        #region Fetch Furniture Information
         public IEnumerator FurnitureInfo( Furniture furniture, System.Action<Furniture> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "furnitureInformation");
            form.AddField("furnitureID", LoginSystem.instance.FurnitureID);
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/furnitureInformation.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // storing the fetched user credentials 
                    string returnedFurniture = www.downloadHandler.text;
                     Debug.Log("returnedFurniture: "+returnedFurniture);
                    // splitting the returned string according to the class attributes : https://csharp-tutorials.com/tr-TR/linq/Split
                    string[] furnitureArray = returnedFurniture.Split(';');

                    // creating a registereduser object in order to store user credentials
                    furniture.FurnitureID = int.Parse(furnitureArray[0]);
                    furniture.Xdimension = float.Parse(furnitureArray[1]);
                    furniture.Ydimension = float.Parse(furnitureArray[2]);
                    furniture.Zdimension = float.Parse(furnitureArray[3]);
                    //furniture.FurnitureTypeID = int.Parse(furnitureArray[4]);
                    //furniture.RoomID = int.Parse(furnitureArray[5]);

                    //checking if the returned values are correct
                    Debug.Log(furniture.Xdimension);
                    Debug.Log(furniture.Ydimension);
                    Debug.Log(furniture.Zdimension);
                    Debug.Log(furniture.FurnitureTypeID);

                    //currentUser = loggedinUser;
                    callback(furniture);
                   

                }
            }


        }
        #endregion

        #region Room Table Connection
        public IEnumerator Room(int UserID, System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "room");
            form.AddField("userid", UserID);

            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/room.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    RoomID = int.Parse(www.downloadHandler.text);
                    LoginSystem.instance.RoomID = RoomID;
                    Debug.Log("deneme roomid:" + RoomID);
                    callback(www.downloadHandler.text);
                }
            }

            yield return new WaitForSeconds(1);
        }

        #endregion

        #region Furniture
        public IEnumerator Furniture(Furniture furnitureMeasurement, string furnitureName){
        WWWForm form = new WWWForm();
            form.AddField("unity", "furniture");
            form.AddField("height", furnitureMeasurement.Ydimension.ToString());
            form.AddField("width", furnitureMeasurement.Xdimension.ToString());
            form.AddField("length", furnitureMeasurement.Zdimension.ToString());
            form.AddField("furnitureName", furnitureName);
            form.AddField("roomID", LoginSystem.instance.RoomID);
            Debug.Log(LoginSystem.instance.RoomID);
        
            //LoginSystem.instance.furnitureID = furnitureMeasurement.furnitureID;
            string message = "";
            Text notificationTxt = GameObject.Find("Canvas/notification").GetComponent<Text>();


            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/furniture.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    message = "" + www.error;
                    notificationTxt.gameObject.SetActive(true);
                    notificationTxt.text = message;
                }
                else
                {
                    // if there are no errors then user account is created:

                    if (www.downloadHandler.text.Contains("Measures saved successfully"))
                    {
                        message = "" + www.downloadHandler.text;
                 
                
                        notificationTxt.gameObject.SetActive(true);
                        notificationTxt.text= message;
                        yield return new WaitForSeconds(1);
                        //UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
                        
                    }
                    else
                    {
                        message = "" + www.downloadHandler.text;
                        notificationTxt.gameObject.SetActive(true);
                        notificationTxt.text = message;
                    }
                }
            }

            yield return new WaitForSeconds(1);
        }


        #endregion

        #region Furniture Type

        public IEnumerator furnitureType()
        {
            string allFurnitureNames = "bed_1;bed_2;bed_3;cabinet_1;cabinet_1_2;cabinet_1_3;cabinet_2;cabinet_3;PFB_BedsideTable;PFB_BedsideTable_2;PFB_FreestandMirror;Queen_Bed_2;Single_Bed_1;Single_Bed_1_2;armchair_1;armchair_2;armchair_3;coffee_table_1;coffee_table_2;coffee_table_3;dining_chair;kitchen_chair_1;kitchen_table_1;kitchen_table_2;kitchen_table_3;PFB_TV;rack_5;sofa;sofa_2;sofa_3;torchere_3;tv_table_1;chair_1;chair_2;modular_table_1;modular_table_1_2;rack_1;rack_2;table_1;table_2;table_3;table_3_2;torchere_2;Door(Brown);window1(single)";
            WWWForm form = new WWWForm();
            form.AddField("unity", "furnitureType");
            form.AddField("allFurnitureNames", allFurnitureNames);

            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/furnitureType.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }

            yield return new WaitForSeconds(1);
        }
        #endregion

        #region Wall Table Information Insertion
        public IEnumerator Wall(Wall wall, System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "wall");
            form.AddField("roomID", wall.RoomID);
            form.AddField("wallName", wall.WallName);
            form.AddField("wallLength", wall.WallLength.ToString().Replace(",", ".")); //since db accepts "." for values which are not integers we are converting "," to ".".
            form.AddField("wallHeight", wall.WallHeight.ToString().Replace(",", "."));

            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/wall.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    callback(www.downloadHandler.text);
                }
            }

            yield return new WaitForSeconds(1);
        }
        #endregion

        #region Room Structure

        public IEnumerator RoomStructure(string wallName, string StructureName, RoomStructure newRoomStructure, RoomStructureLocation newLocation)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "roomstructure");
            form.AddField("StrructureLength", newRoomStructure.StrructureLength.ToString().Replace(",", "."));
            form.AddField("StrructureWidth", newRoomStructure.StrructureWidth.ToString().Replace(",", "."));
            form.AddField("RedDotDistance", newRoomStructure.RedDotDistance.ToString().Replace(",", "."));
            form.AddField("GroundDistance", newRoomStructure.GroundDistance.ToString().Replace(",", "."));
            form.AddField("StructureName", StructureName);
            form.AddField("wallName", wallName);
            form.AddField("roomID", LoginSystem.instance.RoomID);
            form.AddField("LocationX",newLocation.LocationX.ToString().Replace(",", "."));
            form.AddField("LocationY",newLocation.LocationY.ToString().Replace(",", "."));
            form.AddField("LocationZ",newLocation.LocationZ.ToString().Replace(",", "."));
            form.AddField("RotationX",newLocation.RotationX.ToString().Replace(",", "."));
            form.AddField("RotationY", newLocation.RotationY.ToString().Replace(",", "."));
            form.AddField("RotationZ", newLocation.RotationZ.ToString().Replace(",", "."));
            Debug.Log(wallName);
            Debug.Log(LoginSystem.instance.RoomID);
            Debug.Log(StructureName);

            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/roomStructures.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    
                }
            }

            yield return new WaitForSeconds(1);
        }



        #endregion

        #region Fetch Wall Information
        public IEnumerator WallInformation(string []allWalls, System.Action<List<Wall>> callback)
        {
            //List<Wall> wallList = new List<Wall>();
            for (int i = 0; i < allWalls.Length; i++)
            {
                WWWForm form = new WWWForm();
                form.AddField("unity", "wallInformation");
                form.AddField("roomID", LoginSystem.instance.RoomID);
                form.AddField("wallName", allWalls[i].ToString());

                using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/wallInformation.php", form))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        // storing the fetched user credentials 
                        string returnedWall = www.downloadHandler.text;
                        Debug.Log("returnedWall: " + returnedWall);
                        // splitting the returned string according to the class attributes : https://csharp-tutorials.com/tr-TR/linq/Split
                        string[] wallArray = returnedWall.Split(';');

                        wallList.Add(new Wall() { WallID = int.Parse(wallArray[0]), WallName = wallArray[1], WallLength = float.Parse(wallArray[2]), WallHeight = float.Parse(wallArray[3]), RoomID = int.Parse(wallArray[4]) });

                        //currentUser = loggedinUser;

                    }
                }
            }
            callback(wallList);

        }
        #endregion

        #region Fetch Room Structure

        public IEnumerator RoomStructuresInformation(int WallID,System.Action<List<RoomStructure>> callback)
        {
            List<RoomStructure> structuresList = new List<RoomStructure>();
 
            WWWForm form = new WWWForm();
            form.AddField("unity", "roomStructuresInformation");
            form.AddField("wallID", WallID);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/roomStructuresInformation.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string returnedStructure = www.downloadHandler.text;
                    Debug.Log("returnedStructure: " + returnedStructure + " wallid:" + WallID);
                    // splitting the returned string according to the class attributes : https://csharp-tutorials.com/tr-TR/linq/Split
                    if(returnedStructure != "")
                    {
                        string[] structuresArray = returnedStructure.Split(';');
                        int i = 0;

                        while (i < structuresArray.Length/7)
                        {
                            structuresList.Add(new RoomStructure()
                            {
                                RoomStructureID = int.Parse(structuresArray[i]),
                                StrructureLength = float.Parse(structuresArray[i + 1]),
                                StrructureWidth = float.Parse(structuresArray[i + 2]),
                                RedDotDistance = float.Parse(structuresArray[i + 3]),
                                GroundDistance = float.Parse(structuresArray[i + 4]),
                                FurnitureTypeID = int.Parse(structuresArray[i + 5]),
                                WallID = int.Parse(structuresArray[i + 6])
                            });
                            i += 7;
                        }


                    }

                }
            }
            
            callback(structuresList);

        }


        #endregion

        #region roomStructureLocationFetch
        public IEnumerator RoomStructureLocationInformation(int roomStructureID, System.Action<RoomStructureLocation> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "roomStructuresLocationFetch");
            form.AddField("roomStructureID", roomStructureID);
            RoomStructureLocation newRoomStructureLocation = new RoomStructureLocation();

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/roomStructureLocationFetch.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string returnedStructure = www.downloadHandler.text;
                    Debug.Log(returnedStructure);
                    // splitting the returned string according to the class attributes : https://csharp-tutorials.com/tr-TR/linq/Split
                    if (returnedStructure != "")
                    {
                        string[] structuresArray = returnedStructure.Split(';');

                        newRoomStructureLocation.LocationX = float.Parse(structuresArray[0]);
                        newRoomStructureLocation.LocationY = float.Parse(structuresArray[1]);
                        newRoomStructureLocation.LocationZ = float.Parse(structuresArray[2]);
                        newRoomStructureLocation.RotationX = float.Parse(structuresArray[3]);
                        newRoomStructureLocation.RotationY = float.Parse(structuresArray[4]);
                        newRoomStructureLocation.RotationZ = float.Parse(structuresArray[5]);
                        newRoomStructureLocation.RoomStructureID = int.Parse(structuresArray[6]);
                        newRoomStructureLocation.RoomStructureLocationID = int.Parse(structuresArray[7]);
                    }
                }
            }
            callback(newRoomStructureLocation);
        }

        #endregion

        #region Get Furniture Name
        public IEnumerator getFurnitureName(int furnitureID,System.Action<string> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "getFurnitureType");
            form.AddField("furnitureID", furnitureID);

            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/getFurnitureName.php", form))
            {
                yield return www.SendWebRequest();

                // This part of the code checks whether there exists a network or connection error with the database.
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    callback(www.downloadHandler.text);

                }
            }
            yield return new WaitForSeconds(1);
        }

        #endregion
    }
}
