using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fungus;

public class Save : MonoBehaviour
{
    public Flowchart startFlowchart;
    public Flowchart shopFlowchart;
    string fungusisFristName = "isFrist";
    public bool fungusisFrist
    {
        get
        {
            return startFlowchart.GetBooleanVariable(fungusisFristName);
        }
        set
        {
            startFlowchart.SetBooleanVariable(fungusisFristName, value);
        }
    }

    public float goldCount;
    public float crystalCount;
    public bool isStartFungus;
    void Start()
    {
        goldCount = PlayerPrefs.GetFloat("Gold");
        crystalCount = PlayerPrefs.GetFloat("crystal");
    }


    public void SaveGame(float goldCount, float crystalCount, bool isDied)
    {
        PlayerPrefs.SetFloat("Gold", goldCount);
        PlayerPrefs.SetFloat("crystal", crystalCount);
        PlayerPrefs.SetFloat("isDied", Convert.ToInt32(isDied));

        PlayerPrefs.SetFloat("StartFungus", Convert.ToInt32(fungusisFrist));
    }

    public void SavePassiveSkil(string skillName)
    {
        PlayerPrefs.SetInt(skillName, 1);
    }
}
