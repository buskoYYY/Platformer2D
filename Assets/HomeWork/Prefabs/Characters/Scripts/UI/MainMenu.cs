using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;

    [SerializeField] private SettingsWindow _settingsWindow;
    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
        _settingsButton.onClick.AddListener(_settingsWindow.Open);
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
        _startButton.onClick.RemoveListener(_settingsWindow.Open);
    }

    private void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
