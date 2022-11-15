using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingMenu;
    bool isSteeingMenu;
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnSteeing()
    {
        isSteeingMenu = true;
        settingMenu.SetActive(isSteeingMenu);
    }

    public void OnClosureSteeing()
    {
        isSteeingMenu = false;
        settingMenu.SetActive(isSteeingMenu);
    }
}
