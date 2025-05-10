using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Localization
{
    public static Localization Instance;
    private static Dictionary<string, string> _translation;

    public static void InitTranslation()
    {
        if (YandexGame.EnvironmentData.language == "en")
        {
            _translation = new()
            {
                {"Way of the  warrior", "Way of the  warrior" }
            };
        }
    }
}
