using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> Maps = new List<GameObject>{};//儲存地圖
    int[] randomint;//洗牌用陣列
    int nowMaps = 0;
    public float enemyCount = 0;
    public Player player;
    void Start()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            //把地圖放進陣列中
            GameObject gameObject = this.gameObject.transform.GetChild(i).gameObject;
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

    public void SwapMaps()//紀錄切換地圖
    {
        if(nowMaps < (randomint.Length - 1))
        {
            nowMaps++;
        }else
        {
            nowMaps = 0;
        }

        Vector3 outSetPos = Maps[randomint[nowMaps]].GetComponent<Map>().outSetObj.transform.position;//起始點位置
        Vector3 endPos = Maps[randomint[nowMaps]].GetComponent<Map>().endObj.transform.position;//終點位置

        // 設定玩家的位置
        player.transform.position = new Vector3(outSetPos.x,player.transform.position.y,outSetPos.z);

        Maps[randomint[nowMaps]].GetComponent<Map>().endObj.SetActive(false);

        enemyCount =  Maps[randomint[nowMaps]].GetComponent<Map>().EnemyPos.Count;
        
        Maps[randomint[nowMaps]].GetComponent<Map>().eventTrigger.gameObject.SetActive(true);

        // 複製要生成敵人的位置給eventTrigger
        Maps[randomint[nowMaps]].GetComponent<Map>().eventTrigger.GetComponent<EventTrigger>().CopyList(Maps[randomint[nowMaps]].GetComponent<Map>());
        Debug.Log(Maps[randomint[nowMaps]]);
    }

    public void EnemyCount()
    {
        enemyCount--;
        Debug.Log(enemyCount);
        if(enemyCount == 0)
        {
            Maps[randomint[nowMaps]].GetComponent<Map>().endObj.SetActive(true);
        }

    }
}
