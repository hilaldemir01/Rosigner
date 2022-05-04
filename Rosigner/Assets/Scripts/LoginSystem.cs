using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Models;

public class LoginSystem :  MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public Button login;
    public Text notificationTxt;
    
    //Database connection
    RosignerContext db = new RosignerContext();

    // to pass user credentials to other scenes
    public RegisteredUser loggedinUser;
    public int RoomID;


    // https://www.youtube.com/watch?v=BZjmqMd-4vo&ab_channel=CocoCode for passing variables

    public static LoginSystem instance; // this is for creating only one instance at a time

    // https://forum.unity.com/threads/getting-return-value-of-a-coroutine.837550/ : callbacks from coroutines
    void Start() 
    {
        Level.PreviousLevel = "Login";
        notificationTxt=GameObject.Find("Canvas/notification").GetComponent<Text>();
        notificationTxt.gameObject.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        string email= emailInput.text;
        string password=passwordInput.text;
        
        StartCoroutine(db.Login(email, password, LoginMessage));
        yield return new WaitForSeconds(1);
        //if(SceneManager.GetActiveScene().Equals("PreviousDesigns"))
        //{
            StartCoroutine(db.LoginUserInfo(email,loggedinUser, fetchUserInformation));

       // }
    }
    public void LoginMessage (string message)
    {
        notificationTxt.text = " "+message;
    }

    // This is for getting user credentials after the login process is successfully made and storing into the global variable
    public void fetchUserInformation (RegisteredUser newUser)
    {
        loggedinUser.FirstName = newUser.FirstName;
        loggedinUser.LastName = newUser.LastName;
        loggedinUser.Email = newUser.Email;
        loggedinUser.Gender = newUser.Gender;
    }
}
