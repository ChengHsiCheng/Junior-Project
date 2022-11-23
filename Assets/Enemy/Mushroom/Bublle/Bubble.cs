using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed;
    Enemy enemy;
    float damege;
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().PlayerBeAttack(damege);
        }
    }

    public void SteDamege(float _damege)
    {
        damege = _damege;
    }
}
