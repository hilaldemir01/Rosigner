using Assets.Models;
using UnityEngine;
using UnityEngine.UI;

public class UserMainPage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text welcomeText;
    public Text textField;
    RegisteredUser loggedinUser = new RegisteredUser();

    void Start()
    {
      //  string userName = currentUser.FirstName + currentUser.LastName;
        welcomeText = GameObject.Find("Canvas/SidebarPanel/WelcomeText").GetComponent<Text>();

        // get user credentials from here
        loggedinUser = LoginSystem.instance.loggedinUser;

        welcomeText.text = "Welcome \n" + loggedinUser.FirstName + " " + loggedinUser.LastName + "!";

    }

    public void onButtonClicked()
    {
        // in this method, we are going to generate a new roomid in the database in order to create a new design. 
        UnityEngine.SceneManagement.SceneManager.LoadScene("WallCreate");

    }
    public void logoutButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
