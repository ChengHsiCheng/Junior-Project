using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldUI : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    public Text goldQuantity;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        goldQuantity.text = "X " + player.goldQuantity.ToString();
    }
}
