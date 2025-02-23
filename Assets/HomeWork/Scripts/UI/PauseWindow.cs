using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : PauseWindowBase
{
    [SerializeField] private Button _contineButton;
    protected override void OnEnable()
    {
        base.OnEnable();
        _contineButton.onClick.AddListener(Continue);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _contineButton.onClick.RemoveListener(Continue);
    }
    private void Continue()
    {
        gameObject.SetActive(false);
    }
}
