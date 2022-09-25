using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<EnemyStatusInfo>Enemy = new List<EnemyStatusInfo>{};
    public List<GameObject>GeneratePos = new List<GameObject>{};
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            Debug.Log(GeneratePos.Count);
            for(int i =0;i < GeneratePos.Count;i++)
            {
                Instantiate(Enemy[Random.Range(0,Enemy.Count)],GeneratePos[i].transform.position,Quaternion.Euler(0,0,0));
            }
        }
    }
    public void CopyList(Map map)
    {
        GeneratePos = new List<GameObject>(map.EnemyPos);
    }
}
