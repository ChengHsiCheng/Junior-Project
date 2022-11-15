using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCrystalUI : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    public Text crystalQuantity;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        crystalQuantity.text = "X " + player.crystalCount.ToString();
    }
}
