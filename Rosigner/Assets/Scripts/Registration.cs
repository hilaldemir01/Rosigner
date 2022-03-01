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
        form.AddField("password", passwordInput2.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/userRegister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
            }
        }
    }

    public void VerifyInputs()
    {
        register.interactable = (nameInput.text.Length < 50 && passwordInput1.text.Length >= 8 && passwordInput1.text == passwordInput2.text);

    }
}