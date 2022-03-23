using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{

    public InputField nameInput;
    public InputField surnameInput;
    public Dropdown genderInput;
    public InputField emailInput;
    public InputField passwordInput1;
    public InputField passwordInput2;
    public Button register;
    public Text notificationTxt;
    

  void Start() {
        
        notificationTxt=GameObject.Find("Canvas/notification").GetComponent<Text>();
        notificationTxt.gameObject.SetActive(false);

    } 

    public void CallRegister()
    {
        StartCoroutine(Register());

    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "register");
        form.AddField("firstname", nameInput.text);
        form.AddField("lastname", surnameInput.text);
        form.AddField("gender", GameObject.Find("GenderInput").GetComponent<Dropdown>().value);
        form.AddField("email", emailInput.text);
        form.AddField("password", passwordInput1.text);
        form.AddField("password1", passwordInput2.text);
        notificationTxt.gameObject.SetActive(true);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userRegister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notificationTxt.text= "" + www.error;
            }
            else
            {
                if(www.downloadHandler.text.Contains("Registration is successful")){
                    notificationTxt.text= "" + www.downloadHandler.text;
                    yield return new WaitForSeconds(1);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
                }else{
                    notificationTxt.text="" + www.downloadHandler.text;
                }
            }
        }
    }

   public async void VerifyInputs()
    {
        if(nameInput.text.Length >0 && surnameInput.text.Length > 0 && emailInput.text.Length > 0 && genderInput.value != 0 && passwordInput1.text.Length >= 8 &&  passwordInput2.text.Length >= 8)
        {
            register.interactable = true;
        }
    }
}