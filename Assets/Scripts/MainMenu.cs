using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject accessibilityOptionsPanel;
    
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnAccessibilityToggleChanged(bool isOn)
    {
        AccessibilityManager.Instance.accessibilityEnabled = isOn;
        
        CheckDisabler();
        
        // Show or hide the tool settings panel
        accessibilityOptionsPanel.SetActive(isOn);
        
    }
    
    public void OnAccessibilityFogToggleChanged(bool isOn)
    {
        AccessibilityManager.Instance.disableFog = isOn;
        RenderSettings.fog = !isOn;
    }

    public void CheckDisabler()
    {
        if (!AccessibilityManager.Instance.accessibilityEnabled)
        {
            AccessibilityManager.Instance.DisableAccessibilityMode();
        }
    }
    
    public void OnAccessibilityIconsToggleChanged(bool isOn)
    {
        AccessibilityManager.Instance.iconsAboveObjects = isOn;
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // for debug
        {
            FindObjectOfType<FontScaler>()?.ApplyLargeFonts();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            FindObjectOfType<FontScaler>()?.ApplyDefaultFonts();
        }
    }
    */
}
