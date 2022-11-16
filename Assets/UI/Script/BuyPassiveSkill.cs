using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyPassiveSkill : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void OnBuyPassiveSkill(int n)
    {
        Debug.Log(n);
        if (n == 1 && 25 <= player.crystalCount)
        {
            player.addPositiveSkill(new LifeSteal());
            player.crystalCount -= 25;
        }
        else if (n == 2 && 25 <= player.crystalCount)
        {
            player.addPositiveSkill(new NextRoomHealth());
            player.crystalCount -= 25;
        }
        else if (n == 3 && 15 <= player.crystalCount)
        {
            player.addPositiveSkill(new Berserker());
            player.crystalCount -= 15;
        }
        else if (n == 4 && 15 <= player.crystalCount)
        {
            player.addPositiveSkill(new InitialGold());
            player.crystalCount -= 15;
        }
        else if (n == 5 && 20 <= player.crystalCount)
        {
            player.addPositiveSkill(new IncreaseMaxHp());
            player.crystalCount -= 20;
        }
        else if (n == 6 && 20 <= player.crystalCount)
        {
            player.addPositiveSkill(new IncreaseDamage());
            player.crystalCount -= 20;
        }
        else
        {
            return;
        }
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

}
