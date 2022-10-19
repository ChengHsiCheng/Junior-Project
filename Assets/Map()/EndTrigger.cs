using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public MapController mapController;
    bool isTrigger;
    public LevelUI ui;

    EnemyParmType ranParmType;
    float ranParmValue;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(isTrigger)
        {
            ui.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {

                mapController.SwapMaps(1, ranParmType, ranParmValue);

                //刪除場上所有掉落物
                GameObject[] FlopItem;
                FlopItem = GameObject.FindGameObjectsWithTag("FlopItem");
                for(int i = 0; i < FlopItem.Length ; i++)
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
        if(other.tag == "Player")
        {
            isTrigger = true;
            ui.InTrigger(ranParmType, ranParmValue);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isTrigger = false;
            ui.gameObject.SetActive(false);
        }
    }

    public void RanEnemyParm()
    {
        ranParmType = (EnemyParmType)Random.Range(0, 3);
        ranParmValue = Random.Range(1, 10);

        Debug.Log(ranParmType + " " + ranParmValue);
    }
    
}
