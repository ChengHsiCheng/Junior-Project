using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemEffect;
    public GameObject ShopUI;
    public GameObject obj;
    public bool isEnter;
    public float price;

    Player player;

    void Start()
    {
    }

    void Update()
    {
        obj.transform.Rotate(0, 20 * Time.deltaTime, 0);

        if (isEnter && player.goldCount >= price)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyItem(player);

                player.goldCount -= price;

                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player")
        {
            ShopUI.gameObject.SetActive(true);
            ShopUI.GetComponent<ShopUI>().SetText(itemEffect);
            player = other.GetComponent<Player>();

            isEnter = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShopUI.gameObject.SetActive(false);

            isEnter = false;
        }

    }

    public virtual void BuyItem(Player player)
    {

    }

}
