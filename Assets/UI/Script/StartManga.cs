using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class StartManga : MonoBehaviour
{
    public Image manga01;
    public Image manga02;
    public Image manga03;
    public Image manga04;
    int mangaCount;
    float mangaTimer;

    public Image black;
    float blackTimer;

    AudioSource source;
    public AudioClip BGM;



    void Start()
    {
        black.color = new Color(0, 0, 0, 1);

        manga01.color = new Color(1, 1, 1, 0);
        manga02.color = new Color(1, 1, 1, 0);
        manga03.color = new Color(1, 1, 1, 0);
        manga04.color = new Color(1, 1, 1, 0);

        source = GetComponent<AudioSource>();
        source.clip = BGM;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (blackTimer < 3 && mangaCount == 0)
        {
            black.color = new Color(0, 0, 0, 1 - blackTimer);
            blackTimer += Time.deltaTime;
        }

        if (mangaCount < 7)
        {
            mangaTimer += Time.deltaTime;

            if (mangaTimer > 3)
            {
                mangaCount += 1;
                mangaTimer = 0;
            }
        }
        else
        {
            SceneManager.LoadScene(2);
        }

        if (mangaCount == 1)
        {
            manga01.color = new Color(1, 1, 1, mangaTimer / 2);
        }
        else if (mangaCount == 2)
        {
            manga02.color = new Color(1, 1, 1, mangaTimer / 2);
        }
        else if (mangaCount == 3)
        {
            manga03.color = new Color(1, 1, 1, mangaTimer / 2);
        }
        else if (mangaCount == 4)
        {
            manga04.color = new Color(1, 1, 1, mangaTimer / 2);
        }
        else if (mangaCount == 6)
        {
            black.color = new Color(0, 0, 0, 1 - blackTimer / 2);
            blackTimer -= Time.deltaTime;
        }

        if (Input.anyKey)
        {
            if (mangaCount <= 4)
            {
                black.color = new Color(0, 0, 0, 0);
                manga01.color = new Color(1, 1, 1, 1);
                manga02.color = new Color(1, 1, 1, 1);
                manga03.color = new Color(1, 1, 1, 1);
                manga04.color = new Color(1, 1, 1, 1);

                blackTimer = 2;

                mangaCount = 5;
            }
        }
    }
}
