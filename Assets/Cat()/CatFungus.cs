using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;
using UnityEngine.Assertions;

public class CatFungus : MonoBehaviour
{
    public Flowchart flowchart;
    public bool fungusIsLeave
    {
        get
        {
            return flowchart.GetBooleanVariable("isLeave");
        }
        set
        {
            flowchart.SetBooleanVariable("isLeave", value);
        }
    }

    public AudioClip roar01;
    public AudioClip roar02;
    AudioSource audioSource;

    public float turnSpeed = 1;
    public float moveSpeed = 1;

    Animator animator;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fungusIsLeave)
        {
            if (transform.rotation.y > 0)
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);

                transform.position += transform.forward * moveSpeed * Time.deltaTime;

                if (transform.position.z >= 9)
                {
                    gameObject.SetActive(false);
                    return;
                }

                animator.SetBool("Walk", true);
            }
        }
    }

    public void PlayAudio()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
            audioSource.PlayOneShot(roar01);
        else if (i == 1)
            audioSource.PlayOneShot(roar02);
    }
}
