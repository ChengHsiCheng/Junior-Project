using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeigger : MonoBehaviour
{
    public MapController mapController;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(other.tag == "Player")
        {
            mapController.RandomInt();
            mapController.SwapMaps();

            //刪除場上所有掉落物
            GameObject[] FlopItem;
            FlopItem = GameObject.FindGameObjectsWithTag("FlopItem");
            for(int i = 0; i < FlopItem.Length ; i++)
            {
                Destroy(FlopItem[i].gameObject);
            }
        }
    }
}
