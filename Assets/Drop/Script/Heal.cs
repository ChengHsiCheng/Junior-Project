using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float value;
    public Heal()
    {
        value += 10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().PlayerHeal(value);
            Destroy(gameObject);
        }
    }
}
