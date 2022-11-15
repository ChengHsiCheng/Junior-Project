using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelUp : MonoBehaviour
{
    public GameObject UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject, 0.1f);
        }
    }

}
