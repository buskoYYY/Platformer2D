using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Button _backButton;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Toggle _musicSwitcher;
    [SerializeField] private Toggle _soundSwitcher;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Close);
        _musicVolume.onValueChanged.AddListener(ChangeVolumeMusic);
        _soundVolume.onValueChanged.AddListener(ChangeVolumeSound);
        _musicSwitcher.onValueChanged.AddListener(SwitchMuteMusic);
        _soundSwitcher.onValueChanged.AddListener(SwitchMuteSound);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(Close);
        _musicVolume.onValueChanged.RemoveListener(ChangeVolumeMusic);
        _soundVolume.onValueChanged.RemoveListener(ChangeVolumeSound);
        _musicSwitcher.onValueChanged.RemoveListener(SwitchMuteMusic);
        _soundSwitcher.onValueChanged.RemoveListener(SwitchMuteSound);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _musicSwitcher.isOn = SaveService.MusicIsOn;
        _soundSwitcher.isOn = SaveService.SoundIsOn;
        _musicVolume.value = SaveService.MusicVolume;
        _soundVolume.value = SaveService.SoundVolume;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        SaveService.Save();
    }

    private void ChangeVolumeMusic(float value)
    {
        SaveService.SetMusicVolume(value);
        _audioManager.RefreshSettings();
    }

    private void ChangeVolumeSound(float value)
    {
        SaveService.SetSoundVolume(value);
        _audioManager.RefreshSettings();
    }

    private void SwitchMuteMusic(bool isOn)
    {
        SaveService.SetMusicIsOn(isOn);
        _audioManager.RefreshSettings();
    }

    private void SwitchMuteSound(bool isOn)
    {
        SaveService.SetSoundIsOn(isOn);
        _audioManager.RefreshSettings();
    }
}
