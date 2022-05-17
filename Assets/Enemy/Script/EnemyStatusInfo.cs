using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusInfo : MonoBehaviour
{
    public float Hp;
    public float damege;
    public GameObject heal;
    public MapController mapController;
    public float dieTime = 0;
    public int state = 0;//0 = 追逐 , 1 = 攻擊 , 2 = 受擊

    public void Face(GameObject player)//面相玩家
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }

    public void Damege(float damege ,bool isRepulse)//計算傷害
    {
        Hp -= damege;
        if(Hp <= 0)
        {
            state = 3;
            
        }else
        {
            if(isRepulse)
            {
                state = 2;
            }
        }
    }
}
