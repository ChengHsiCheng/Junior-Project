using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

public class ShopFungus : MonoBehaviour
{

    public Flowchart startFlowchart;
    string fungusBoolName = "isDied";
    public bool fungusBool
    {
        get
        {
            return startFlowchart.GetBooleanVariable(fungusBoolName);
        }
        set
        {
            startFlowchart.SetBooleanVariable(fungusBoolName, value);
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {

        fungusBool = GameObject.Find("Player").GetComponent<Player>().isDied;
        Debug.Log(fungusBool);

    }
}
