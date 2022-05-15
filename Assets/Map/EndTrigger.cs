using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public MapController mapController;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            mapController.SwapMaps();
        }
    }
}
