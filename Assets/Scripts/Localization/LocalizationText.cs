using System;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
[AddComponentMenu("UI/LocalizationText", 10)]
public class LocalizationText : Text
{
    protected override void Awake()
    {
        base.Awake();
        LocalizationEvent.AddListener(LocalizationEventType.ChangeLanguage, OnLocalize);
        OnLocalize();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        LocalizationEvent.RemoveListener(LocalizationEventType.ChangeLanguage, OnLocalize);
    }

    public string key;

    public string Key
    {
        get
        {
            return key;
        }
        set
        {
            key = value;
            OnLocalize();
        }
    }

    public void OnLocalize()
    {
        text = Localization.GetValue(key);
    }
}