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

    public void CallLogin()
    {
        StartCoroutine(Login());

    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "login");
        form.AddField("email", emailInput.text);
        form.AddField("password", passwordInput.text);
        


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userLogin.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text.Contains("Login success!")){
                    Debug.Log(www.downloadHandler.text);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                }else {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}
