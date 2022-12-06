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

    AudioSource source;
    public AudioClip click;

    private void Start()
    {
        black.color = new Color(0, 0, 0, 0);
        source = GetComponent<AudioSource>();
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
        source.PlayOneShot(click);
        onStart = true;
    }

    public void OnExitGame()
    {
        source.PlayOneShot(click);
        Application.Quit();
    }

}
