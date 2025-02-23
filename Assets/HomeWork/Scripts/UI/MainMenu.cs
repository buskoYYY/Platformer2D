using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
