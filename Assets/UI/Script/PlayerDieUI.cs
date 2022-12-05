using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDieUI : MonoBehaviour
{
    Image image;
    float timer;
    Player player;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
            image.color = new Color(1, 1, 1, 0 + (timer));
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                image.color = new Color(1, 1, 1, 0);
                timer = 0;
                player.isDying = false;
                player.PlayerDie();
                gameObject.SetActive(false);
            }

        }



    }
}
