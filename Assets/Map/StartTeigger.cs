using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeigger : MonoBehaviour
{
    public MapController mapController;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            mapController.RandomInt();
            mapController.SwapMaps();
        }
    }
}
