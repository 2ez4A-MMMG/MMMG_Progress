using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Buttons : MonoBehaviour
{
    public void StartButton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void RulesButton(string RulesLevel)
    {
        SceneManager.LoadScene(RulesLevel);
    }

    public void CreditButton(string CreditLevel)
    {
        SceneManager.LoadScene(CreditLevel);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MenuButton(string BacktoMenu)
    {
        SceneManager.LoadScene(BacktoMenu);
    }

}
