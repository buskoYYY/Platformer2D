using UnityEngine;
using UnityEngine.UI;
using YG;

public class MobileTutorial : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    [SerializeField] private Image _movePointer;
    [SerializeField] private TouchHandler _joystick;

    private void Awake()
    {
        if (Application.isEditor && _isMobile == false|| YandexGame.EnvironmentData.isDesktop)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _joystick.Down += OnDown;
    }

    private void OnDisable()
    {
        _joystick.Down += OnDown;
    }

    private void OnDown()
    {
        _joystick.Down -= OnDown;
        _movePointer.gameObject.SetActive(false);
    }
}
