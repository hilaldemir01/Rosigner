using Assets.Models;
using System.Collections;
using System.Text.RegularExpressions;
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
    RosignerContext db = new RosignerContext();

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
        // This part of the code is used to check whether e-mail is valid or not. For e-mail address to be valid, it should include "@" sign and after that
        // there should be a "." sign following by a domain.

        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(emailInput.text);
        int passwordCheck = VerifyPassword();
        // If e-mail address is valid, then :
        if (match.Success || passwordCheck == 1)
        {

            if(passwordInput1.text == passwordInput2.text)
            {
                RegisteredUser newUser = new RegisteredUser();

                newUser.FirstName = nameInput.text;
                newUser.LastName = surnameInput.text;
                newUser.Gender = GameObject.Find("GenderInput").GetComponent<Dropdown>().value;
                newUser.Email = emailInput.text;
                newUser.Hash = passwordInput1.text;

            
                notificationTxt.gameObject.SetActive(true);
                StartCoroutine(db.Register(newUser)) ;
            }
            else
            {
                notificationTxt.gameObject.SetActive(true);
                notificationTxt.text = "Passwords Do Not Match";
            }

            
            // if the e-mail formats do not fit the regex expression then display an error message:

        }
        else if (!(match.Success) || passwordCheck == 0)
        {
            notificationTxt.gameObject.SetActive(true);
            notificationTxt.text = "Invalid E-mail Type";
        }
        yield return new WaitForSeconds(1);

    }

        // This method is used to check whether the password includes numbers, upper or lower letters, special characters and at least 8 digit long.
        public int VerifyPassword()
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,50}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(passwordInput1.text))
        {
            notificationTxt.text = "Password should contain at least one lower case letter";
            return 0;
        }
        else if (!hasUpperChar.IsMatch(passwordInput1.text))
        {
            notificationTxt.text = "Password should contain at least one upper case letter"; 
            return 0;

        }
        else if (!hasMiniMaxChars.IsMatch(passwordInput1.text))
        {
            notificationTxt.text = "Password should not be less than 8 characters";
            return 0;

        }
        else if (!hasNumber.IsMatch(passwordInput1.text))
        {
            notificationTxt.text = "Password should contain at least one numeric value";
            return 0;

        }
        else if (!hasSymbols.IsMatch(passwordInput1.text))
        {
            notificationTxt.text = "Password should contain At least one special case characters";
            return 0;
        }

        return 1;

    }

    // This method is used to check whether input fields are empty or not. It also checks for the password lengths to be more than 8 digits.
    public async void VerifyInputs()
    {
        if(nameInput.text.Length >0 && surnameInput.text.Length > 0 && emailInput.text.Length > 0 && genderInput.value != 0 && passwordInput1.text.Length >= 8 &&  passwordInput2.text.Length >= 8 && GameObject.Find("GenderInput").GetComponent<Dropdown>().value != 0)
        {
            register.interactable = true;
        }
    }
}