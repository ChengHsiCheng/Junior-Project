using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    Player player;
    float damege;
    float explodeDamege;
    public Vector3 playerPos;
    void Start()
    {
        transform.GetComponent<Collider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);

            transform.position += (playerPos - transform.up * 0.5f) * Time.deltaTime;
        }
    }

    public void GetPlayer(Player _player, float _damege)
    {
        player = _player;
        damege = _damege;
        explodeDamege = damege * 0.5f;

    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            player.PlayerBeAttack(damege);

        }

        transform.GetComponent<Collider>().enabled = true;
        Invoke("ClosureTrigger", 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            player.PlayerBeAttack(explodeDamege);

        }
    }

    void ClosureTrigger()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
