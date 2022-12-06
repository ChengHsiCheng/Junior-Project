using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTriffer : MonoBehaviour
{
    public Map endMap;
    public GameObject UI;

    public MapController mapController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            UI.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.position = new Vector3(endMap.outSetObj.transform.position.x, other.transform.position.y, endMap.outSetObj.transform.position.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            UI.SetActive(false);
    }

    public void bossDie()
    {
        mapController.source.Stop();
    }
}
