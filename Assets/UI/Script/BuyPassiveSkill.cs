using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyPassiveSkill : MonoBehaviour
{
    Player player;

    public GameObject lifeStealButtle;
    public GameObject nextRoomHealthButtle;
    public GameObject berserkerButtle;
    public GameObject initialGoldButtle;
    public GameObject increaseMaxHpButtle;
    public GameObject increaseDamageButtle;

    AudioSource source;
    public AudioClip click;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        source = GetComponent<AudioSource>();
        Open();
    }
    public void OnBuyPassiveSkill(int n)
    {

        source.PlayOneShot(click);
        if (n == 1 && 25 <= player.crystalCount)
        {
            player.addPositiveSkill(new LifeSteal());
            player.crystalCount -= 45;
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

    public void ExitButton()
    {
        gameObject.SetActive(false);
        source.PlayOneShot(click);

        Time.timeScale = 1;
    }

    public void Open()
    {
        for (int i = 0; i < player.passiveSkills.Count; i++)
        {
            Debug.Log(player.passiveSkills[i].ToString());
            if (player.passiveSkills[i].ToString() == "LifeSteal")
            {
                Debug.Log(player.passiveSkills[i].ToString());
                lifeStealButtle.gameObject.SetActive(false);
            }
            else if (player.passiveSkills[i].ToString() == "NextRoomHealth")
            {
                nextRoomHealthButtle.SetActive(false);
            }
            else if (player.passiveSkills[i].ToString() == "Berserker")
            {
                berserkerButtle.SetActive(false);
            }
            else if (player.passiveSkills[i].ToString() == "InitialGold")
            {
                initialGoldButtle.SetActive(false);
            }
            else if (player.passiveSkills[i].ToString() == "IncreaseMaxHp")
            {
                increaseMaxHpButtle.SetActive(false);
            }
            else if (player.passiveSkills[i].ToString() == "IncreaseDamage")
            {
                increaseDamageButtle.SetActive(false);
            }
        }
    }

}
