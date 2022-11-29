using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillTrigger : MonoBehaviour
{
    public GameObject passiveSkillUI;
    public GameObject text;
    bool isEnter;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEnter)
        {
            passiveSkillUI.SetActive(true);

            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEnter = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEnter = false;
            text.SetActive(false);
        }

    }
}
