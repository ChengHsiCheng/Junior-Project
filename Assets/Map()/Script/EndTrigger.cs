using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    Player player;
    public MapController mapController;
    bool isTrigger;
    public LevelUI ui;

    LevelRewardType ranLevelRewardType;
    EnemyParmType ranParmType;
    float ranParmValue;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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

                mapController.SwapMaps(1, ranParmType, ranParmValue);

                player.levelRewardType = ranLevelRewardType;
                player.CheckPassiveSkills("OnNextRoom");

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
        if (other.tag == "Player")
        {
            isTrigger = true;
            ui.InTrigger(ranParmType, ranParmValue, ranLevelRewardType, player.gameObject);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.gameObject.transform.parent.tag == "Player")
        {
            isTrigger = false;
            ui.gameObject.SetActive(false);
        }
    }

    public void RanEnemyParm()
    {
        ranParmType = (EnemyParmType)Random.Range(0, 3);
        ranParmValue = Random.Range(1, 10);

        ranLevelRewardType = (LevelRewardType)Random.Range(0, 2);

    }

}
