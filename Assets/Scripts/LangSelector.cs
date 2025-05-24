using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LangSelector : MonoBehaviour
{
    private bool isActive = false;
    
    public void ChangeLang(int id)
    {
        if (isActive) return;
        StartCoroutine(SetLocale(id));
    }
    IEnumerator SetLocale(int id)
    {
        //id = lang
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        isActive = false;
    }
}
