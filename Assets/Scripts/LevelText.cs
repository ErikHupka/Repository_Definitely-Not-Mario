using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelText : MonoBehaviour
{
    SceneFunctionsManager sceneFunctionsManager;
    [SerializeField] TextMeshProUGUI levelText;

    private void Awake()
    {
        sceneFunctionsManager = FindObjectOfType<SceneFunctionsManager>();
    }

    private void Start()
    {
        levelText.text = "Level: " + sceneFunctionsManager.GetCurrentScene();
    }
}
