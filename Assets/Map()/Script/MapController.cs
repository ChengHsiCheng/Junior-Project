using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject map;
    public List<GameObject> Maps = new List<GameObject> { }; // 儲存地圖
    int[] randomint;//洗牌用陣列
    int nowMaps = 0;
    public int mapsCount = 0;
    public float enemyCount = 0;
    public Player player;
    public Map shotMap;
    public Map bossMap;
    Map nowMap;
    public EndTrigger endTrigger01;
    public EndTrigger endTrigger02;

    public GameObject goldObj;
    public GameObject crystalObj;
    public GameObject skillUpObj;
    public GameObject healObj;

    float enemyHpAddition;
    float enemyDamegeAddition;
    float enemySpeedAddition;
    void Start()
    {
        for (int i = 0; i < map.transform.childCount; i++)
        {
            //把地圖放進陣列中
            GameObject gameObject = map.gameObject.transform.GetChild(i).gameObject;
            Maps.Add(gameObject);
        }
        // 設定洗牌陣列大小
        randomint = new int[Maps.Count];


    }

    public void RandomInt()// 將地圖洗牌
    {
        for (int i = 0; i < randomint.Length; i++)
        {
            // 初始化陣列
            randomint[i] = i;
        }
        int temp, rndTemp;

        System.Random rnd = new System.Random();

        for (int i = 0; i < randomint.Length; i++)
        {
            //產生一個亂數
            rndTemp = rnd.Next(0, randomint.Length);

            //將i位置上的牌 和產生的隨機randomint位置上的牌 交換
            temp = randomint[i];
            randomint[i] = randomint[rndTemp];
            randomint[rndTemp] = temp;
        }
    }

    public void SwapMaps(int i, EnemyParmType ranParmType, float ranParmValue) // 紀錄切換地圖
    {
        nowMap = Maps[randomint[nowMaps]].GetComponent<Map>();



        if (mapsCount == 4)
        {
            nowMap = shotMap;

            nowMap.GetComponent<Shop>().AddItem();
        }
        else if (mapsCount == 5)
        {
            nowMap = bossMap;
        }
        else
        {
            if (nowMaps == Maps.Count - 1)
            {
                nowMaps = 0;
            }
            else
            {
                nowMaps++;
            }
        }

        mapsCount++;



        // 起始點位置
        Vector3 outSetPos = nowMap.outSetObj.transform.position;
        // 終點位置
        Vector3 endPos01 = nowMap.endObj01.transform.position;
        Vector3 endPos02 = nowMap.endObj02.transform.position;

        nowMap.enemyObj.gameObject.SetActive(true);

        endTrigger01.transform.position = endPos01;
        endTrigger01.gameObject.SetActive(false);
        endTrigger02.transform.position = endPos02;
        endTrigger02.gameObject.SetActive(false);

        // 設定玩家的位置
        player.transform.position = new Vector3(outSetPos.x, player.transform.position.y, outSetPos.z);

        if (nowMap == shotMap)
        {
            EnemyClear();
        }


        if (i == 1)
        {

            if (ranParmType == EnemyParmType.Hp)
            {
                enemyHpAddition += ranParmValue;
            }
            else if (ranParmType == EnemyParmType.Damege)
            {
                enemyDamegeAddition += ranParmValue;
            }
            else if (ranParmType == EnemyParmType.Speed)
            {
                enemySpeedAddition += ranParmValue;
            }

            nowMap.enemyObj.SetParmValue(enemyHpAddition, enemyDamegeAddition, enemySpeedAddition);

        }
        else if (i == 0)
        {
            enemyHpAddition = 0;
            enemyDamegeAddition = 0;
            enemySpeedAddition = 0;
            nowMap.enemyObj.SetParmValue(enemyHpAddition, enemyDamegeAddition, enemySpeedAddition);
            Debug.Log("enemyHpAddition + enemyDamegeAddition + enemySpeedAddition");
        }


    }

    public void EnemtCheck()
    {
        GameObject[] enemysCount = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemysCount.Length == 0)
        {
            EnemyClear();
        }
    }

    public void EnemyClear()
    {
        endTrigger01.gameObject.SetActive(true);
        endTrigger01.RanEnemyParm();
        endTrigger02.gameObject.SetActive(true);
        endTrigger02.RanEnemyParm();

        if (nowMap != shotMap && nowMap != bossMap)
        {
            if (player.levelRewardType == LevelRewardType.Gold)
            {
                Instantiate(goldObj, player.transform.position, Quaternion.identity);
            }
            else if (player.levelRewardType == LevelRewardType.Crystal)
            {
                Instantiate(crystalObj, player.transform.position, Quaternion.identity);
            }
            else if (player.levelRewardType == LevelRewardType.SkillUp)
            {
                Instantiate(skillUpObj, player.transform.position, Quaternion.identity);
            }
            else if (player.levelRewardType == LevelRewardType.Heal)
            {
                Instantiate(healObj, player.transform.position, Quaternion.identity);
            }
        }
    }

}
