using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject LoginSystem;
    public void PlayGame()
    {
        LoginSystem.SetActive(true);
        MainMenu.SetActive(false);
    
    }
    public void QuitGame()
    {
        
        Application.Quit();
    }
}
