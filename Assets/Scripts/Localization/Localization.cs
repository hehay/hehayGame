using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Localization
{
    public static SystemLanguage[] languages =
    {
        SystemLanguage.English,
        SystemLanguage.Chinese,
        SystemLanguage.French,
        SystemLanguage.German,
        SystemLanguage.Russian,
        SystemLanguage.Japanese,
        SystemLanguage.Spanish,
        SystemLanguage.Portuguese,
        SystemLanguage.Italian
    };
    private static List<LocalizationData> localizationDatas = new List<LocalizationData>();

    public static SystemLanguage curLanguage;

    public static void Init()
    {
        curLanguage = (SystemLanguage)PlayerPrefs.GetInt("Language", (int)Application.systemLanguage);
        TextAsset asseet = Resources.Load<TextAsset>("Localization/Localization");
        localizationDatas = JsonUtility.FromJson<Serialization<LocalizationData>>(asseet.text).ToList();
    }
    
    public static string GetValue(string key)
    {
#if !UNITY_EDITOR
        if (localizationDatas.Count == 0)
#endif
        Init();

        string value = "";

        foreach (var v in localizationDatas)
        {
            if (v.Key == key)
            {
                switch (curLanguage)
                {
                    case SystemLanguage.English:
                        value = v.English;
                        break;
                    case SystemLanguage.Chinese:
                        value = v.Chinese;
                        break;
                    case SystemLanguage.French:
                        value = v.French;
                        break;
                    case SystemLanguage.German:
                        value = v.German;
                        break;
                    case SystemLanguage.Russian:
                        value = v.Russian;
                        break;
                    case SystemLanguage.Japanese:
                        value = v.Japanese;
                        break;
                    case SystemLanguage.Spanish:
                        value = v.Spanish;
                        break;
                    case SystemLanguage.Portuguese:
                        value = v.Portuguese;
                        break;
                    case SystemLanguage.Italian:
                        value = v.Italian;
                        break;
                    default:
                        value = v.English;
                        break;
                }
            }
        }

        return value;
    }

    public static void SetLanguage(SystemLanguage language)
    {
        curLanguage = language;

        PlayerPrefs.SetInt("Language", (int)curLanguage);

        LocalizationEvent.BroadCast(LocalizationEventType.ChangeLanguage);
    }
}

[Serializable]
public class LocalizationData
{
    public string Key;
    public string English;
    public string Chinese;
    public string French;
    public string German;
    public string Russian;
    public string Japanese;
    public string Spanish;
    public string Portuguese;
    public string Italian;
}