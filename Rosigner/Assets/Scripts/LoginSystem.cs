using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginSystem : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public Button login;
    public Text notificationTxt;

    void Start() {

        
        notificationTxt=GameObject.Find("Canvas/notification").GetComponent<Text>();
        notificationTxt.gameObject.SetActive(false);

    } 

    public void CallLogin()
    {
        StartCoroutine(Login());

    }

    // This method is used to connect to the database and compare user credentials which are e-mail and password to log in the user to the system.
    IEnumerator Login()
    {
      
        WWWForm form = new WWWForm();
        form.AddField("unity", "login");
        form.AddField("email", emailInput.text);
        form.AddField("password", passwordInput.text);
        notificationTxt.gameObject.SetActive(true);
        
        // database connection is done here:
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userLogin.php", form))
        {
            // checking if there are any database connection errors:

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notificationTxt.text= "" + www.error;
            }
            else
            {
                // if users credentials find a match in the database, then users can log in to their accounts 

                if(www.downloadHandler.text.Contains("Login success!")){
                    
                    //Debug.Log(www.downloadHandler.text);
                    notificationTxt.text= "" + www.downloadHandler.text;
                    yield return new WaitForSeconds(1);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                }else {
                    notificationTxt.text="" + www.downloadHandler.text;
                    //Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}
