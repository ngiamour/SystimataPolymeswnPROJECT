using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject accessibilityOptionsPanel;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

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

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("music", Mathf.Log(volume)*20);
        
        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfx", Mathf.Log(volume)*20);
        
        PlayerPrefs.SetFloat("sfxVolume",volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
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
