using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    Canvas pauseGameCanvas;
    SceneFunctionsManager sceneFunctionsManager;

    private void Awake()
    {
        pauseGameCanvas = FindObjectOfType<UIButtons>().GetComponent<Canvas>();
        sceneFunctionsManager = FindObjectOfType<SceneFunctionsManager>();

        pauseGameCanvas.enabled = false;
    }

    public void PauseGame(InputAction.CallbackContext context) //Player pressing "P" or "Escape"
    {
        if (context.started)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseGameCanvas.enabled = true;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseGameCanvas.enabled = false;
            }
        }
    }

    public void RestartLevel(InputAction.CallbackContext context) //Player pressing "R"
    {
        if (context.started)
        {
            sceneFunctionsManager.RestartLevel();
        }
    }
}
