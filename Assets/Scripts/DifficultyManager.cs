using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    
    public void changeDif(bool isOn)
    {
        Debug.Log("TOGGLE CHANGED: " + isOn);
        if (isOn)
        {
            SetEasy();
        }
        else
        {
            SetDefault();
        }
    }

    public void SetEasy()
    {
        AccessibilityManager.Instance.easyMode = true;
        Debug.Log("It's easy dif now");
        PlayerPrefs.SetInt("EasyMode", 1);
    }

    public void SetDefault()
    {
        AccessibilityManager.Instance.easyMode = false;
        Debug.Log("It's hard dif now");
        PlayerPrefs.SetInt("EasyMode", 0);
    }
}
