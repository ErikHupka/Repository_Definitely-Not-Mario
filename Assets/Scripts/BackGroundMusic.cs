using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameMusic;

    //Components
    AudioSource audioSource;

    private void Awake()
    {
        DontDestroyCheck();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMusicPerScene(int scene)
    {
        if (scene == 0)
        {
            audioSource.Stop();
            audioSource.clip = mainMenuMusic;
            audioSource.PlayDelayed(0.1f);
        }
        else
        {
            audioSource.Stop();
            audioSource.clip = gameMusic;
            audioSource.PlayDelayed(0.1f);
        }
    }



    void DontDestroyCheck()
    {
        if (FindObjectsOfType<BackGroundMusic>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
