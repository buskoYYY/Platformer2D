using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _selectLevelButton;

    [SerializeField] private SettingsWindow _settingsWindow;
    [SerializeField] private SelectLevelWindow _selectLevelWindow;
    [SerializeField] private float _showTime;
    [SerializeField] private CanvasGroup _menuPanel;

    private void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
        _settingsButton.onClick.AddListener(_settingsWindow.Open);
        _selectLevelButton.onClick.AddListener(_selectLevelWindow.Open);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
        _startButton.onClick.RemoveListener(_settingsWindow.Open);
        _selectLevelButton.onClick.RemoveListener(_selectLevelWindow.Open);
    }

    private void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void ShowMenu()
    {
        try
        {
            StartCoroutine(Showing());
        }
        catch(Exception) { }
    }

    private IEnumerator Showing()
    {
        yield return LocalizationSettings.InitializationOperation;

        float time = 0;
        float startAlpha = 0;
        float finishAlpha = 1;

        while(time < _showTime)
        {
            time += Time.deltaTime;
            _menuPanel.alpha = Mathf.Lerp(startAlpha, finishAlpha, time/_showTime);
            yield return null;
        }

        _menuPanel.alpha = finishAlpha;
    }
}
