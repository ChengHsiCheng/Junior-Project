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
    public void OnBuyPassiveSkill(string n)
    {
        if (n == PassiveSkillType.LifeSteal.ToString() && 25 <= player.crystalCount)
        {
            player.addPositiveSkill(new LifeSteal());
            player.crystalCount -= 25;
        }
        else if (n == PassiveSkillType.NextRoomHealth.ToString() && 25 <= player.crystalCount)
        {
            player.addPositiveSkill(new NextRoomHealth());
            player.crystalCount -= 25;
        }
        else if (n == PassiveSkillType.Berserker.ToString() && 15 <= player.crystalCount)
        {
            player.addPositiveSkill(new Berserker());
            player.crystalCount -= 15;
        }
        else if (n == PassiveSkillType.InitialGold.ToString() && 15 <= player.crystalCount)
        {
            player.addPositiveSkill(new InitialGold());
            player.crystalCount -= 15;
        }
        else if (n == PassiveSkillType.IncreaseMaxHp.ToString() && 20 <= player.crystalCount)
        {
            player.addPositiveSkill(new IncreaseMaxHp());
            player.crystalCount -= 20;
        }
        else if (n == PassiveSkillType.IncreaseDamage.ToString() && 20 <= player.comboStep)
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
