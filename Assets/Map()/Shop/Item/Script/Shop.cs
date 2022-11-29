using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> itemPos = new List<GameObject>();
    public List<Item> items = new List<Item>();
    public GameObject cristleItem;
    public GameObject healItem;
    void Start()
    {

    }

    public void AddItem()
    {
        Instantiate(items[Random.Range(0, 3)].gameObject, itemPos[0].transform.position, Quaternion.identity).transform.parent = itemPos[0].transform;
        Instantiate(healItem.gameObject, itemPos[1].transform.position, Quaternion.identity).transform.parent = itemPos[1].transform;
        Instantiate(cristleItem.gameObject, itemPos[2].transform.position, Quaternion.identity).transform.parent = itemPos[2].transform;
    }
    public void DestroyItem()
    {
        for (int i = 0; i < itemPos.Count; i++)
        {
            Destroy(itemPos[i].gameObject.transform.GetChild(0).gameObject);
        }
    }
}
