using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playGameButton() 
    {
        SceneManager.LoadScene(1);
    }

    public void settingsButton()
    {

    }

    public void quitButton()
    {
        Application.Quit();
    }
}
