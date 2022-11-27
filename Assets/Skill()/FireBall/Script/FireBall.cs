using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    bool isShoot;
    public float speed;
    public float damege;
    public float ExpAdd;

    public AudioClip fireAudio;
    public AudioClip shoot;
    public AudioClip boom;
    AudioSource audioSource;

    private void Start()
    {
        transform.GetComponent<Collider>().enabled = false;
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = fireAudio;
        audioSource.Play();
    }

    void Update()
    {
        if (isShoot)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

        }
    }

    public void Charge(Vector3 pos, Quaternion rotate, Vector3 scale)
    {
        // 設定位置、旋轉、大小
        transform.position = pos;
        transform.rotation = rotate;
        transform.localScale = scale;
    }

    public void Shoot(float _damege)
    {
        isShoot = true;
        audioSource.Stop();
        audioSource.PlayOneShot(shoot);
        damege = _damege;
        Destroy(gameObject, 10);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().BeAttacked(damege, true);
            transform.GetComponent<Collider>().enabled = true;
            Invoke("ClosureTrigger", 0.1f);
        }

        audioSource.PlayOneShot(boom);
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.BeAttacked(damege * ExpAdd, true);
            enemy.AddDebuff(EnemyDebuffType.Burning);
        }
        Debug.Log(other.name);
    }

    void ClosureTrigger()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
