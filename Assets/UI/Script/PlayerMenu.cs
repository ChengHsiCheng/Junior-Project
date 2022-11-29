using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject settingMenu;
    bool isSettingMenu = false;
    void Start()
    {

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
}
