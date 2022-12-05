using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusControl : MonoBehaviour
{
    public GameObject start;
    public GameObject firstDie;
    public GameObject noSkillSelected;

    Save save;
    void Start()
    {
        save = GameObject.Find("Player").GetComponent<Save>();

        if (PlayerPrefs.GetFloat("StartFungus") == 1)
        {
            start.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnPlayerFirstDie()
    {
        firstDie.SetActive(true);
    }

    public void OnPlayerNoSkillSelected()
    {
        noSkillSelected.SetActive(true);
    }
}
