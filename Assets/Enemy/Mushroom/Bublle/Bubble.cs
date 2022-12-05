using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed;
    Enemy enemy;
    float damege;

    AudioSource source;
    public AudioClip bubbleFlying;
    public AudioClip bubbleTouch;

    bool isCollision;
    void Start()
    {
        Destroy(gameObject, 10);
        source = GetComponent<AudioSource>();
        source.clip = bubbleFlying;
        source.Play();
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
        if (!isCollision)
        {
            source.PlayOneShot(bubbleTouch);
            isCollision = true;
            source.Stop();
        }

    }

    public void SteDamege(float _damege)
    {
        damege = _damege;
    }
}
