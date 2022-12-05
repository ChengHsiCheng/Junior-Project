using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text parmType;
    public Text value;

    public Image image;
    public Image rewardImage;

    public Sprite attack;
    public Sprite hp;
    public Sprite speed;

    public Sprite gold;
    public Sprite cristle;
    public Sprite heal;
    public Sprite fireSkill;
    public Sprite iceSkill;
    public Sprite nullImage;

    public void InTrigger(EnemyParmType type, float _value, LevelRewardType rewardType, GameObject player)
    {
        if (type == EnemyParmType.Hp)
        {
            parmType.text = "血量".ToString();
            image.sprite = hp;
        }
        else if (type == EnemyParmType.Damege)
        {
            parmType.text = "傷害".ToString();
            image.sprite = attack;
        }
        else if (type == EnemyParmType.Speed)
        {
            parmType.text = "速度".ToString();
            image.sprite = speed;
        }

        if (rewardType == LevelRewardType.Gold)
        {
            rewardImage.sprite = gold;
        }
        else if (rewardType == LevelRewardType.Crystal)
        {
            rewardImage.sprite = cristle;
        }
        else if (rewardType == LevelRewardType.Heal)
        {
            rewardImage.sprite = heal;
        }
        else if (rewardType == LevelRewardType.SkillUp)
        {
            if (player.GetComponent<SkillManager>().skill.SkillName == "FireBall")
            {
                rewardImage.sprite = fireSkill;
            }
            else
            {
                rewardImage.sprite = iceSkill;
            }
        }
        else
        {
            rewardImage.sprite = nullImage;
        }

        value.text = _value.ToString() + "%";
    }
}
