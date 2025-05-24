using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager Instance;

    [Header("Toggles")]
    public bool accessibilityEnabled = false;
    public bool disableFog = false;
    public bool largeFonts = false;
    public bool iconsAboveObjects = false;
    public bool easyMode = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void EnableAccessibilityMode()
    {
        accessibilityEnabled = true;

        /*
        narration = true;
        largeFonts = true;
        colorblindMode = true;
        iconsAboveObjects = true;
        easyMode = true;
        */
        
        // Call other managers to apply settings
  //      FindObjectOfType<FontScaler>()?.ApplyLargeFonts();
        //   NarratorManager.Enable();
       // DifficultyManager.SetEasy();
   //     FindObjectOfType<IconOverlayManager>()?.ShowIcons(true);
    }
    
    public void DisableAccessibilityMode()
    {
        accessibilityEnabled = false;

        disableFog = false;
        largeFonts = false;
        iconsAboveObjects = false;
        easyMode = false;

        FindObjectOfType<FontScaler>()?.ApplyDefaultFonts();
        //   NarratorManager.Disable();
        FindObjectOfType<DifficultyManager>()?.SetDefault();
        FindObjectOfType<IconOverlayManager>()?.ShowIcons(false);
    }

}
