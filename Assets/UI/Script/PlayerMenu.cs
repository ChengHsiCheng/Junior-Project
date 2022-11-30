using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public GameObject settingMenu;
    bool isSettingMenu = false;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isSettingMenu = !isSettingMenu;
            settingMenu.SetActive(isSettingMenu);
            if (isSettingMenu)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void OnClosureSteeing()
    {
        isSettingMenu = false;
        settingMenu.SetActive(isSettingMenu);

        Time.timeScale = 1;
    }

    public void OnBackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
