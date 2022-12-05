using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillTrigger : MonoBehaviour
{
    public GameObject passiveSkillUI;
    public GameObject text;
    bool isEnter;

    public Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEnter)
        {
            passiveSkillUI.SetActive(true);

            //passiveSkillUI.GetComponent<BuyPassiveSkill>().Open();

            player.StopAudio();

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
