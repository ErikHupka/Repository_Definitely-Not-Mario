using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneFunctionsManager : MonoBehaviour
{
    [SerializeField] int restartDelay = 1;
    LevelManagement levelManagement;

    //Components
    BackGroundMusic backGroundMusic;

    private void Awake()
    {
        DontDestroyCheck();
        backGroundMusic = FindObjectOfType<BackGroundMusic>();
        levelManagement = FindObjectOfType<LevelManagement>();
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartWithDelay());
    }

    IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        backGroundMusic.SetMusicPerScene(GetCurrentScene());
    }

    public void RestartLevelWithNoDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        backGroundMusic.SetMusicPerScene(GetCurrentScene());
    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = GetCurrentScene() + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            backGroundMusic.SetMusicPerScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
            backGroundMusic.SetMusicPerScene(0);
        }
    }

    public void LoadLevelWithIndex(int index)
    {
        SceneManager.LoadScene(index);
        backGroundMusic.SetMusicPerScene(index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        backGroundMusic.SetMusicPerScene(0);
    }



    void DontDestroyCheck()
    {
        if (FindObjectsOfType<SceneFunctionsManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
