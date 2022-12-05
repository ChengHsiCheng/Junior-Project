using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Fungus;

public class PlayerMenu : MonoBehaviour
{
    public Flowchart startFlowchart;
    string fungusBoolName02 = "isOver";
    public bool fungusBool02
    {
        get
        {
            return startFlowchart.GetBooleanVariable(fungusBoolName02);
        }
        set
        {
            startFlowchart.SetBooleanVariable(fungusBoolName02, value);
        }
    }

    public GameObject settingMenu;
    bool isSettingMenu = false;

    public Player player;

    public Image black;
    float blackTimer;
    void Start()
    {
        Time.timeScale = 1;
        player = GameObject.Find("Player").GetComponent<Player>();
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

                player.StopAudio();
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (!fungusBool02 && blackTimer < 2)
        {
            black.color = new Color(0, 0, 0, 1 - blackTimer / 2);
            blackTimer += Time.deltaTime;
        }
        Debug.Log(fungusBool02);
        if (fungusBool02)
        {
            black.color = new Color(0, 0, 0, 1 - blackTimer / 2);
            blackTimer -= Time.deltaTime;

            player.SaveGame();

            if (blackTimer < 0)
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    public void OnClosureSteeing()
    {
        isSettingMenu = false;
        settingMenu.SetActive(isSettingMenu);

        Time.timeScale = 1;
    }

    public void OnNewGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(1);
    }

    public void OnBackMainMenu()
    {
        Time.timeScale = 1;
        player.SaveGame();
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        player.SaveGame();
        Application.Quit();
    }
}
