using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowLevel : MonoBehaviour
{
    public MapController mapController;
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mapController.mapsCount == 0)
        {
            _text.text = "";
        }
        else
        {
            _text.text = "第" + mapController.mapsCount + "關";
        }

    }
}
