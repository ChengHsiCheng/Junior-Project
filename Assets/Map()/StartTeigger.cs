using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeigger : MonoBehaviour
{
    Player player;
    public MapController mapController;
    public GameObject ui;
    bool isTrigger;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (isTrigger)
        {
            ui.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                mapController.RandomInt();
                mapController.SwapMaps(0, EnemyParmType.Null, 0);

                player.levelRewardType = LevelRewardType.Null;
                player.CheckPassiveSkills("OnStart");

                //刪除場上所有掉落物
                GameObject[] FlopItem;
                FlopItem = GameObject.FindGameObjectsWithTag("FlopItem");
                for (int i = 0; i < FlopItem.Length; i++)
                {
                    Destroy(FlopItem[i].gameObject);
                }
                isTrigger = false;
                ui.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.transform.parent.tag == "Player")
        {
            isTrigger = true;
        }

    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.transform.parent.tag == "Player")
        {
            isTrigger = false;
            ui.gameObject.SetActive(false);
        }
    }
}
