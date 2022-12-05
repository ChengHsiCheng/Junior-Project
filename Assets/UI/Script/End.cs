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
    float timer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!fungusBool && timer < 2)
        {
            black.color = new Color(0, 0, 0, 1 - timer / 2);
            timer += Time.deltaTime;
        }

        if (fungusBool)
        {
            if (timer > 0)
            {
                black.color = new Color(0, 0, 0, 1 - timer / 2);
                timer -= Time.deltaTime;
                SceneManager.LoadScene(0);
            }

            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }

        }

    }
}
