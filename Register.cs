using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;



public class Register : MonoBehaviour
{
    [SerializeField] GameObject LoginSystem;

    [SerializeField] GameObject MainMenu;
    public TMP_InputField UsernameField;
    public TMP_InputField PasswordField;


    public string username;
    public string password;



    public void Back()
    {
        LoginSystem.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void Start()
    {

    }

    public void CallLogin()
    {
        username = UsernameField.text;
        password = PasswordField.text;
        StartCoroutine(Login());
        
        


        
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username",username);
        form.AddField("password",password);
        string url = "http://localhost/Unity_DB/Login.php";
        WWW www= new WWW (url,form);
        yield return www;
        if (www.text == "Login successful!")
        {
            SceneManager.LoadScene("TheGame");
        }
        else if (www.text == "Something went wrong with the database!")
        {
            Debug.Log("Something went wrong with the database!");
        }
        else if (www.text == "Password incorrect!")
        {
            Debug.Log("Password incorrect!");
        }
        else if (www.text == "Username incorrect!")
        {
            Debug.Log("Username incorrect!");
        }
        else if (www.text == "User doesn't exist, please register!")
        {
            Debug.Log("User doesn't exist, please register!");
        }
        else if(www.text == "Connection failed!")
        {
            Debug.Log("Connection failed!");
        }
        else
        {
            SceneManager.LoadScene("TheGame");
        }
        
    }

    public void CallRegister()
    {
        username = UsernameField.text;
        password = PasswordField.text;
        StartCoroutine(Registering());
         
    }

    IEnumerator Registering()
    {
        WWWForm form = new WWWForm();
        form.AddField("username",username);
        form.AddField("password",password);
        string url = "http://localhost/Unity_DB/Register.php";
        WWW www= new WWW (url,form);
        yield return www;

       if (www.text.Contains("Database created!Created table userlogin!Created table savedgames!Created table GameState!"))
            {
                Debug.Log("Database and tables created!");
            }
            else if (www.text == "Succesfuly created user!")
            {
                Debug.Log("User registered successfully!");
            }
            else if (www.text == "Username already exists!")
            {
                Debug.Log("Username already exists!");
            }
            else if (www.text == "Insert query fail!")
            {
                Debug.LogError("Failed to insert user into database!");
            }
            else
            {
                Debug.LogError("Unknown error occurred: " + www.text);
            }

    }
}