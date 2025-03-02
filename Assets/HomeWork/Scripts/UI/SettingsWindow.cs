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
    private void ChangeVolumeMusic(float value)
    {
        ChangeVolume(value, ConstantData.SaveData.MUSIC_KEY);
    }    
    private void ChangeVolumeSound(float value)
    {
        ChangeVolume(value, ConstantData.SaveData.SOUND_KEY);
    }
    private void ChangeVolume(float value, string key)
    {
        PlayerPrefs.SetFloat(key, value);
        _audioManager.RefreshSettings();
    }
    private void SwitchMuteMusic(bool isOn)
    {
        SwitchMute(isOn, ConstantData.SaveData.MUSIC_MUTE_KEY);
    }    
    private void SwitchMuteSound(bool isOn)
    {
        SwitchMute(isOn, ConstantData.SaveData.SOUND_MUTE_KEY);
    }    
    private void SwitchMute(bool isOn, string key)
    {
        PlayerPrefs.SetInt(key, isOn ? ConstantData.SaveData.IS_ON_VALUE : ConstantData.SaveData.IS_OF_VALUE);
        _audioManager.RefreshSettings();
    }
    public void Open()
    {
        gameObject.SetActive(true);
        _musicSwitcher.isOn = PlayerPrefs.GetInt(ConstantData.SaveData.MUSIC_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) == ConstantData.SaveData.IS_ON_VALUE;
        _soundSwitcher.isOn = PlayerPrefs.GetInt(ConstantData.SaveData.SOUND_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) == ConstantData.SaveData.IS_ON_VALUE;

        _musicVolume.value = PlayerPrefs.GetFloat(ConstantData.SaveData.MUSIC_KEY, ConstantData.SaveData.DEFAULT_VOLUME);
        _soundVolume.value = PlayerPrefs.GetFloat(ConstantData.SaveData.SOUND_KEY, ConstantData.SaveData.DEFAULT_VOLUME);
    }
    public void Close() {
        gameObject.SetActive(false);
    }
}
