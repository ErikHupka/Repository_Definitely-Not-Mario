using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int playerHealth = 3;
    [SerializeField] int playerDamage = 1;
    [SerializeField] int enemiesKilled = 0;

    [Header("Level Stats")]
    [SerializeField] int numberOfEnemies = 0;

    [Header("Hearts Sprites")]
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Image[] hearts;

    [Header("Coins")]
    [SerializeField] Image bronzeCoinSprite;
    [SerializeField] Image silverCoinSprite;
    [SerializeField] Image goldCoinSprite;
    bool bronzeCoinPickedUp = false;
    bool silverCoinPickedUp = false;
    bool goldCoinPickedUp = false;

    [SerializeField] TextMeshProUGUI scoreText;

    //Components
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        ResetHearts();
        SetEnemiesCount(0);
        SetHearthToInactive();
    }


    void ResetHearts()
    {
        foreach (Image heart in hearts)
        {
            heart.sprite = fullHeart;
        }
    }

    public void TakeHitFromEnemy(int damage)
    {
        playerHealth -= damage;
        UpdateHearts();

        if (playerHealth > 0)
        {
            playerMovement.TakeHit();
        }
        else
        {
            playerMovement.DieSequence();
        }
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public int GetPlayerDamage()
    {
        return playerDamage;
    }

    void UpdateHearts()
    {
        if (playerHealth < 0) { return; }

        hearts[playerHealth].sprite = emptyHeart;
    }

    public int GetPlayerScore()
    {
        return enemiesKilled;
    }

    public void SetEnemiesCount(int scoreAddition)
    {
        enemiesKilled += scoreAddition;
        scoreText.text = "Enemies: " + enemiesKilled + " / " + numberOfEnemies;
    }

    void SetHearthToInactive()
    {
        bronzeCoinSprite.color = new Color32(255, 255, 255, 100);
        silverCoinSprite.color = new Color32(255, 255, 255, 100);
        goldCoinSprite.color = new Color32(255, 255, 255, 100);
    }

    public void CoinPickedUp(int coin)
    {
        switch (coin)
        {
            case 0:
                bronzeCoinSprite.color = new Color32(255, 255, 255, 255);
                bronzeCoinPickedUp = true;
                break;
            case 1:
                silverCoinSprite.color = new Color32(255, 255, 255, 255);
                silverCoinPickedUp = true;
                break;
            case 2:
                goldCoinSprite.color = new Color32(255, 255, 255, 255);
                goldCoinPickedUp = true;
                break;
            default:
                break;
        }
    }

    public bool CanExitLevel()
    {
        if (bronzeCoinPickedUp && silverCoinPickedUp && goldCoinPickedUp
            && enemiesKilled >= numberOfEnemies)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
