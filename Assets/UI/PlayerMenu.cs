using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject quitButton;
    bool quitButtonShow = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitButtonShow = !quitButtonShow;
            quitButton.SetActive(quitButtonShow);
            if(quitButtonShow)
            {
                Time.timeScale = 0;
            }else
            {
                Time.timeScale = 1;
            }
        }
    }
}
