using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using YG;

public class EntryPoint : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private List<string> _sceneNames = new();
    [SerializeField] private SelectLevelWindow _selectLevelWindow;

    private void Awake()
    {
        _selectLevelWindow.SetLevelsNames(_sceneNames);
        SaveService.Initialize(_sceneNames);
        SetLanguage();
    }

#if UNITY_EDITOR
    private void Reset()
    {
        int extentionLeanth = 6;
        _sceneNames.Clear();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);

                if (name.StartsWith(LEVEL_SCENE_SUBNAME))
                    _sceneNames.Add(name.Substring(0, name.Length - extentionLeanth));
            }
        }
    }
#endif
    private void SetLanguage()
    {
        try
        {
            StartCoroutine(LoadLocale(YandexGame.EnvironmentData.language));
        }
        catch(Exception) { }
    }

    private IEnumerator LoadLocale(string languageIdentifier)
    {
        yield return LocalizationSettings.InitializationOperation;

        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales[i];

            if(locale.Identifier == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                yield break;
            }
        }
    }
}

