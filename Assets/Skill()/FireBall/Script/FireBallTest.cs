using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTest : MonoBehaviour
{
    bool isShoot;
    public float speed;
    public float damege;

    private void Start()
    {
        transform.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if(isShoot)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void Shoot()
    {
        isShoot = true;
        Destroy(gameObject, 10);
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().BeAttacked(damege);
            transform.GetComponent<Collider>().enabled = true;
            Invoke("ClosureTrigger", 0.1f);
        }

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.tag == "Enemy")
            {
                other.GetComponent<Enemy>().BeAttacked(damege * 2);
            }
        }
    }

    void ClosureTrigger()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
