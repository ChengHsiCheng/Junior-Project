using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Flowchart Flowchart;
    string fungusBoolName = "isEnd";
    public bool fungusBool
    {
        get
        {
            return Flowchart.GetBooleanVariable(fungusBoolName);
        }
        set
        {
            Flowchart.SetBooleanVariable(fungusBoolName, value);
        }
    }
    public Image black;
    public Text thx;
    float timer;
    void Start()
    {
        thx.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fungusBool && timer < 4)
        {
            black.color = new Color(0, 0, 0, 1 - timer / 4);
            timer += Time.deltaTime;
        }

        if (fungusBool)
        {
            if (timer > 0)
            {
                black.color = new Color(0, 0, 0, 1 - timer / 4);
                thx.color = new Color(1, 1, 1, 0 + (4 - timer));
                timer -= Time.deltaTime;
            }
            if (timer > -3)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(0);
            }

            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }

        }

    }
}
