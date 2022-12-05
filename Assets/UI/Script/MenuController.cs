using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    bool isSteeingMenu;

    public Image black;
    public bool onStart;
    public float timer;

    private void Start()
    {
        black.color = new Color(0, 0, 0, 0);

    }

    private void Update()
    {
        if (onStart)
        {
            if (timer < 1)
            {
                black.color = new Color(0, 0, 0, 0 + timer);
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                onStart = false;
                if (Convert.ToBoolean(PlayerPrefs.GetFloat("StartFungus")))
                {
                    SceneManager.LoadScene(2);
                }
                else if (!Convert.ToBoolean(PlayerPrefs.GetFloat("StartFungus")))
                {
                    SceneManager.LoadScene(1);
                }


            }

        }
    }

    public void OnStartGame()
    {
        onStart = true;
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

}
