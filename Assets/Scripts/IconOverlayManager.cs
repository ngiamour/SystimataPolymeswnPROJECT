using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconOverlayManager : MonoBehaviour
{
    public GameObject[] icons; // Assign icons above power-ups, obstacles, etc.

    void Start()
    {
        if (AccessibilityManager.Instance != null &&
            AccessibilityManager.Instance.accessibilityEnabled &&
            AccessibilityManager.Instance.iconsAboveObjects)
        {
            ShowIcons(true);
        }
        else
        {
            ShowIcons(false);
        }
    }

    public void ShowIcons(bool enable)
    {
        foreach (GameObject icon in icons)
        {
            icon.SetActive(enable);
        }
    }
}
