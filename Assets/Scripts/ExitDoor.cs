using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    PlayerStats playerStats;
    LevelManagement levelManagement;
    SceneFunctionsManager sceneFunctionsManager;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        levelManagement = FindObjectOfType<LevelManagement>();
        sceneFunctionsManager = FindObjectOfType<SceneFunctionsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerStats.CanExitLevel())
        {
            levelManagement.SetLevelStatus(sceneFunctionsManager.GetCurrentScene() + 1);
            sceneFunctionsManager.LoadNextLevel();
        }
    }
}
