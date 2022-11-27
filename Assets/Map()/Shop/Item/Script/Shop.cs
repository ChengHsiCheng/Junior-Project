using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> itemPos = new List<GameObject>();
    public List<Item> items = new List<Item>();
    void Start()
    {
        Instantiate(items[0].gameObject, itemPos[0].transform.position, Quaternion.identity);
        Instantiate(items[1].gameObject, itemPos[1].transform.position, Quaternion.identity);
        Instantiate(items[2].gameObject, itemPos[2].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
