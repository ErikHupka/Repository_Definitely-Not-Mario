using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Hover : MonoBehaviour
{
    [SerializeField] AudioClip buttonHoverClip;

    //Components
    AudioSource audioSource;
    Settings settings;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settings = FindObjectOfType<Settings>();
    }

    public void MouseHover()
    {
        audioSource.volume = settings.GetSoundEffectSetting();
        audioSource.PlayOneShot(buttonHoverClip);
    }
}
