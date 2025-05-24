using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
     [Header("Power-Up Durations")]
     [SerializeField] private float powerUpShieldDuration = 10f;
     [SerializeField] private float powerUpDoubleScoreDuration = 10f;
     [SerializeField] private float powerUpMagnetDuration = 10f;
    
        [Header("References")]
        public PowerUpUIManager powerUpUI;
        public GameObject magnetVFX;
        public GameObject shieldVFX;
        public GameObject doubleScoreVFX;
    
        public bool hasMagnet = false;
        public bool hasShield = false;
        public bool hasDoubleScore = false;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
    
        public void ActivatePowerUp(string type)
        {
            audioSource.PlayOneShot(audioClip, 0.5f);
            switch (type)
            {
                case "Magnet":
                    StartCoroutine(MagnetRoutine());
                    break;
                case "Shield":
                    StartCoroutine(ShieldRoutine());
                    break;
                case "DoubleScore":
                    StartCoroutine(DoubleScoreRoutine());
                    break;
            }
        }
    
        IEnumerator MagnetRoutine()
        {
            hasMagnet = true;
            if (magnetVFX)
                magnetVFX.SetActive(true);
            powerUpUI.ShowUI(PowerUpType.Magnet,powerUpMagnetDuration);
            
            yield return new WaitForSeconds(powerUpMagnetDuration);
            
            hasMagnet = false;
            if (magnetVFX)
                magnetVFX.SetActive(false);
            powerUpUI.HideUI(PowerUpType.Magnet);
        }
    
        IEnumerator ShieldRoutine()
        {
            hasShield = true;
            if (shieldVFX)
                shieldVFX.SetActive(true);
            powerUpUI.ShowUI(PowerUpType.Shield,powerUpShieldDuration);
            yield return new WaitForSeconds(powerUpShieldDuration);
            
            hasShield = false;
            if (shieldVFX)
                shieldVFX.SetActive(false);
            powerUpUI.HideUI(PowerUpType.Shield);
        }
    
        IEnumerator DoubleScoreRoutine()
        {
            hasDoubleScore = true;
            if (doubleScoreVFX)
                doubleScoreVFX.SetActive(true);
            powerUpUI.ShowUI(PowerUpType.DoubleScore,powerUpDoubleScoreDuration);
            yield return new WaitForSeconds(powerUpDoubleScoreDuration);
            
            hasDoubleScore = false;
            if (doubleScoreVFX)
                doubleScoreVFX.SetActive(false);
            powerUpUI.HideUI(PowerUpType.DoubleScore);
        }
}
