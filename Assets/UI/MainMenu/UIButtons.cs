using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    //Main Menu
    [Header("ONLY at Main Menu")]
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas chooseLevelCanvas;
    [SerializeField] Canvas settingsCanvas;


    //Pause Menu
    Canvas pauseMenuCanvas;

    //Components
    SceneFunctionsManager sceneFunctionsManager;
    Settings settings;
    LevelManagement levelManagement;

    //Components Settings
    [Header("ONLY at Settings")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    //Load Level Index
    [Header("ONLY at Choose Level")]
    [SerializeField] int levelIndex = 0;
    [SerializeField] List<GameObject> isLocked;
    [SerializeField] List<Button> levelButton;

    private void Awake()
    {
        pauseMenuCanvas = FindObjectOfType<UIButtons>().GetComponent<Canvas>();
        sceneFunctionsManager = FindObjectOfType<SceneFunctionsManager>();
        settings = FindObjectOfType<Settings>();
        levelManagement = FindObjectOfType<LevelManagement>();
    }

    // ---------- Main Menu ----------

    public void OpenChooseLevel()
    {
        // Locked / Unlocked levels
        for (int i = 0; i < levelManagement.GetNumberOfLevels(); i++)
        {
            if (levelManagement.GetLevelStatus(i + 1) == true)
            {
                isLocked[i].SetActive(false);
                levelButton[i].interactable = true;
            }
            else
            {
                isLocked[i].SetActive(true);
                levelButton[i].interactable = false;
            }
        }

        mainCanvas.enabled = false;
        chooseLevelCanvas.enabled = true;
    }

    public void CloseChooseCanvas()
    {
        chooseLevelCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void LoadFirstLevel()
    {
        sceneFunctionsManager.LoadNextLevel();
    }

    public void LoadLevel()
    {
        sceneFunctionsManager.LoadLevelWithIndex(levelIndex);
    }

    public void OpenSettings()
    {
        settingsCanvas.enabled = true;
        mainCanvas.enabled = false;

        musicSlider.value = settings.GetBackgroundMusicSetting();
        soundSlider.value = settings.GetSoundEffectSetting();
    }

    public void SaveSettingsDontClose()
    {
        settings.SetBackgroundMusicSetting(musicSlider.value);
        settings.SetSoundEffectSetting(soundSlider.value);
    }

    public void SaveSettings()
    {
        settingsCanvas.enabled = false;
        mainCanvas.enabled = true;

        settings.SetBackgroundMusicSetting(musicSlider.value);
        settings.SetSoundEffectSetting(soundSlider.value);
    }

    public void DiscardSettings()
    {
        settingsCanvas.enabled = false;
        mainCanvas.enabled = true;
    }


    // ---------- Pause game ----------

    public void ResumeGame()
    {
        pauseMenuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        sceneFunctionsManager.RestartLevelWithNoDelay();
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        sceneFunctionsManager.LoadMainMenu();
        Time.timeScale = 1;
    }
}
