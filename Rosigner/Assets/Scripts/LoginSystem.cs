using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Assets.Models;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public Button login;
    public Text notificationTxt;
    RosignerContext db = new RosignerContext();

    void Start() {

        
        notificationTxt=GameObject.Find("Canvas/notification").GetComponent<Text>();
        notificationTxt.gameObject.SetActive(false);

    } 

    public void CallLogin()
    {
        StartCoroutine(Login());

    }

    IEnumerator Login()
    {
        string email= emailInput.text;
        string password=passwordInput.text;
        StartCoroutine(db.Login(email, password));
        yield return new WaitForSeconds(1);
        if(SceneManager.GetActiveScene().Equals("Menu"))
        {
            StartCoroutine(db.LoginUserInfo(email));

        }
    }
}
