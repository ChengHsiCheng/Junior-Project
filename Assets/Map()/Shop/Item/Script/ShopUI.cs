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
        Imagetocanvas();
        gameObject.SetActive(false);
        rect = GetComponent<RectTransform>();
        //rect.position = new Vector3(0, 0, 0);
    }

    public void SetText(string _itemEffect)
    {
        itemEffect.text = _itemEffect;
    }

    void Imagetocanvas() //把image放到canvas上
    {
        canvas = GameObject.Find("PlayerUI");
        transform.SetParent(canvas.transform);
    }
}
