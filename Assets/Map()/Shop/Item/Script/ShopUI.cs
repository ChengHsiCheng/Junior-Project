using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    GameObject canvas;
    RectTransform rect;
    public Text itemEffect;


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string _itemEffect)
    {
        itemEffect.text = _itemEffect;
    }
}
