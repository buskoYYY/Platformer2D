using System;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public bool MusicIsOn = true;
    public bool SoundIsOn = true;
    public float MusicVolume = ConstantData.SaveData.DEFAULT_VOLUME;
    public float SoundVolume = ConstantData.SaveData.DEFAULT_VOLUME;
    public List<string> UnlockedLevels = new() { "Level1" };
}
