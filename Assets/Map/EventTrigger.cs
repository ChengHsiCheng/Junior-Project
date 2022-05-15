using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject>GeneratePos = new List<GameObject>{};
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            for(int i =0;i < GeneratePos.Count;i++)
            {
            Instantiate(enemy,GeneratePos[i].transform.position,Quaternion.Euler(0,0,0));
            }
        }
    }
    public void CopyList(Map map)
    {
        GeneratePos = new List<GameObject>(map.EnemyPos);
    }
}
