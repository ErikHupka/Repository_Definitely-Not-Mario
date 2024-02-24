using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float backgroundMusicInput = 0.5f;
    [SerializeField] float soundEffectInput = 0.5f;

    //Components
    AudioSource backGroundMusic;

    private void Awake()
    {
        DontDestroyCheck();
        backGroundMusic = FindObjectOfType<BackGroundMusic>().GetComponent<AudioSource>();
    }

    private void Start()
    {
        backGroundMusic.volume = backgroundMusicInput;
    }

    public float GetBackgroundMusicSetting()
    {
        return backgroundMusicInput;
    }

    public void SetBackgroundMusicSetting(float setting)
    {
        backgroundMusicInput = setting;
        backGroundMusic.volume = backgroundMusicInput;
    }

    public float GetSoundEffectSetting()
    {
        return soundEffectInput;
    }

    public void SetSoundEffectSetting(float setting)
    {
        soundEffectInput = setting;
    }

    void DontDestroyCheck()
    {
        if (FindObjectsOfType<Settings>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
