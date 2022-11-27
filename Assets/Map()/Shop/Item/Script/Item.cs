using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemEffect;
    public GameObject ShopUI;
    public GameObject obj;

    void Start()
    {
    }

    void Update()
    {
        obj.transform.Rotate(0, 20 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player")
        {
            ShopUI.gameObject.SetActive(true);
            ShopUI.GetComponent<ShopUI>().SetText(itemEffect);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShopUI.gameObject.SetActive(false);
        }

    }

}