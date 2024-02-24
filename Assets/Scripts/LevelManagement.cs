using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] int numberOfLevels = 9;
    [SerializeField] Dictionary<int, bool> levelsUnlocked = new Dictionary<int, bool>(); // Key: Level number; Value: isUnlocked

    private void Awake()
    {
        DontDestroyCheck();
    }

    private void Start()
    {
        for (int i = 1; i <= numberOfLevels; i++) // Create an empty ditionary with all levels
        {
            levelsUnlocked.Add(i, false);
        }

        levelsUnlocked[1] = true; //Unlock level 1
    }

    void DontDestroyCheck()
    {
        if (FindObjectsOfType<LevelManagement>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLevelStatus(int level)
    {
        levelsUnlocked[level] = true;
    }

    public bool GetLevelStatus(int levelIndex)
    {
        return levelsUnlocked[levelIndex];
    }

    public int GetNumberOfLevels()
    {
        return numberOfLevels;
    }
}
