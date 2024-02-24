using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin type")]
    [SerializeField] int coinType = 0;

    bool isPickedUp = false;

    //Components
    PlayerStats playerStats;
    AudioSource audioSource;
    Settings settings;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        audioSource = GetComponent<AudioSource>();
        settings = FindObjectOfType<Settings>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            playerStats.CoinPickedUp(coinType);
            GetComponent<SpriteRenderer>().enabled = false;
            audioSource.volume = settings.GetSoundEffectSetting();
            audioSource.Play();
            Destroy(gameObject, 2);
        }
    }
}
