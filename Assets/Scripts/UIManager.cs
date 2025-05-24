using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioSource _audioSource;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private GameObject gameWonObject;
    [SerializeField] private GameObject gameWonUI;
    
    private bool stopOnce=false;
    private PlayerMove _playerMove;
    [SerializeField] private GameObject soundsToStop;
    
    [Header("Hover Sound")]
    public AudioClip _audioClip;
    private float lastHoverTime;
    [SerializeField] private float hoverCooldown = 0.2f;

    [Header("Click Sound")] public AudioClip clickClip;

    private WinManager _winManager;
    public bool showWin = false;

    private bool disableFog = false;
    private void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        _winManager = FindObjectOfType<WinManager>();

        if (AccessibilityManager.Instance != null)
        {
            RenderSettings.fog = !AccessibilityManager.Instance.disableFog;
        }
    }

    private void Update()
    {
        //Game Over
        if (_playerMove.gameOver)
        {
            _playerMove.enabled = false;
            scoreObject.SetActive(false);
            gameOverObject.SetActive(true);
            soundsToStop.SetActive(false);
        }

        if (showWin == true)
        {
            gameWonUI.SetActive(true);
        }
        //Time.timeScale = 0;
    }

    public void showWinFoo()
    {
        gameWonObject.SetActive(true);
        scoreObject.SetActive(false);
        showWin = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartSpecific(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void HoverSound()
    {
        if (Time.time - lastHoverTime > hoverCooldown)
        {
            _audioSource.PlayOneShot(_audioClip, 0.4f);
            Debug.Log("Hover Sound");
            lastHoverTime = Time.time;
        }
    }
    
    public void ClickSound()
    {
        _audioSource.PlayOneShot(clickClip, 1.0f);
    }
}
