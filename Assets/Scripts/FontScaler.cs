using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontScaler : MonoBehaviour
{
    public int fontSizeIncrease = 10;
    private bool isLarge = false;

    public void ToggleFontSize(bool isOn)
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>(true);

        foreach (var tmp in texts)
        {
            if (isOn && !isLarge)
            {
                tmp.fontSize += fontSizeIncrease;
                AccessibilityManager.Instance.largeFonts = true;
            }
            else if (!isOn && isLarge)
            {
                tmp.fontSize -= fontSizeIncrease;
                AccessibilityManager.Instance.largeFonts = false;
            }
        }

        isLarge = isOn;
    }
}
