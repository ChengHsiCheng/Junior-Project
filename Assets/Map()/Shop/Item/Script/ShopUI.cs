using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    GameObject canvas;
    RectTransform rect;
    public Text itemEffect;
    public Text itemPrice;

    public int price;


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string _itemEffect, int _price)
    {
        itemEffect.text = _itemEffect;
        itemPrice.text = _price.ToString() + "元";
    }
}
