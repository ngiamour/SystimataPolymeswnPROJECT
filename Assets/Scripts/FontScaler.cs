using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontScaler : MonoBehaviour
{
    [System.Serializable]
    public class TMPFontEntry
    {
        public TMP_Text textComponent;
        public float normalSize = 28f;
        public float largeSize = 36f;
        public bool boldInAccessibility = true;
    }

    public List<TMPFontEntry> entries;

    public void changeFont(bool isOn)
    {
        Debug.Log("TOGGLE CHANGED: " + isOn);
        if (isOn==true)
        {
            ApplyLargeFonts();
        }
        else
        {
            ApplyDefaultFonts();
        }
    }
    
    public void ApplyLargeFonts()
    {
        AccessibilityManager.Instance.largeFonts = true;
        foreach (var entry in entries)
        {
            entry.textComponent.fontSize = entry.largeSize;

            if (entry.boldInAccessibility)
                entry.textComponent.fontStyle |= FontStyles.Bold;
        }
    }

    public void ApplyDefaultFonts()
    {
        AccessibilityManager.Instance.largeFonts = false;
        foreach (var entry in entries)
        {
            entry.textComponent.fontSize = entry.normalSize;

            // Remove bold if it was applied
            entry.textComponent.fontStyle &= ~FontStyles.Bold;
        }
    }
}
