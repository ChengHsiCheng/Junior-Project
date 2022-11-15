using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text parmType;
    public Text value;
    public Text reward;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InTrigger(EnemyParmType type, float _value, LevelRewardType rewardType)
    {
        if (type == EnemyParmType.Hp)
        {
            parmType.text = "血量".ToString();
        }
        else if (type == EnemyParmType.Damege)
        {
            parmType.text = "傷害".ToString();
        }
        else if (type == EnemyParmType.Speed)
        {
            parmType.text = "速度".ToString();
        }

        value.text = _value.ToString() + "%";
        reward.text = rewardType.ToString();
    }
}
