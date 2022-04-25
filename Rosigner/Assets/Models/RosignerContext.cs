using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Models
{
    class RosignerContext
    {
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
                        notificationTxt.text= message;
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

        public IEnumerator Login(string email, string password)
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
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

                }
                else
                {
                    // if users credentials find a match in the database, then users can log in to their accounts 

                    if (www.downloadHandler.text.Contains("Login success!"))
                    {

                        //Debug.Log(www.downloadHandler.text);
                 //      notificationTxt2.gameObject.SetActive(true);
                 //      notificationTxt2.text = "" + www.downloadHandler.text;
                        
                        yield return new WaitForSeconds(1);
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                    }
                    else
                    {
                        //      notificationTxt2.gameObject.SetActive(true);
                        //     notificationTxt2.text = "" + www.downloadHandler.text;
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");

                        //Debug.Log(www.downloadHandler.text);
                    }
                }
            }
        }
        public IEnumerator Measurement(Measurement furnitureMeasurement){
            WWWForm form = new WWWForm();
            form.AddField("unity", "measurement");
            form.AddField("height", furnitureMeasurement.furniture_height);
            form.AddField("width", furnitureMeasurement.furniture_width);
            form.AddField("length", furnitureMeasurement.furniture_length);
            form.AddField("furnitureName", furnitureMeasurement.furniture_name);

            string message = "";
            Text notificationTxt = GameObject.Find("Canvas/notification").GetComponent<Text>();


            // setting database connection:
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/measurement.php", form))
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
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
                        
                    }
                    else
                    { //sublime
                        message = "" + www.downloadHandler.text;
                        notificationTxt.gameObject.SetActive(true);
                        notificationTxt.text = message;
                    }
                }
            }

            yield return new WaitForSeconds(1);
        }








    }
}
