using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioPickupSound;
    [SerializeField] private AudioClip coinPickupClip;

    public int totalCoins = 0;

    [SerializeField] private TextMeshProUGUI coinsText;
    
    
    
    [Header("Spawner")]
    
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float repeatRate = 2;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPos;
    private PlayerMove _playerMove;
    private WinManager _winManager;
    
    public GameObject iconBillboardPrefab;

    private void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        _winManager = FindObjectOfType<WinManager>();
        coinsText.text = totalCoins.ToString();
        InvokeRepeating("SpawnCoin",startDelay,repeatRate);
       // PickedUp(0);
    }

    public void PickedUp(int scoreToAdd)
    {
        PowerUps powerUps = FindObjectOfType<PowerUps>();
        if (powerUps != null && powerUps.hasDoubleScore)
        {
            scoreToAdd *= 2;
        }
        totalCoins+=scoreToAdd;
        coinsText.text = totalCoins.ToString();
        audioPickupSound.PlayOneShot(coinPickupClip);
    }

    void SpawnCoin()
    {
        if (_playerMove.gameOver == false && _winManager.gameWon==false && _winManager.stopSpawning == false)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            
            GameObject spawnedObject = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation);
        
            TryAttachIcon(spawnedObject);

            Vector3 CalculateSpawnPosition()
            {
                var position = spawnPos.position;
                float x = _playerMove.transform.position.x;
                float z = position.z;
                float y = Random.Range(0.4f, 4f);

                return new Vector3(x, y, z);
            }
        }
    }
    
    private void TryAttachIcon(GameObject target)
    {
        if (AccessibilityManager.Instance != null && AccessibilityManager.Instance.iconsAboveObjects && iconBillboardPrefab != null)
        {
            GameObject icon = Instantiate(iconBillboardPrefab);
            Transform visualChild = target.transform.childCount > 0 ? target.transform.GetChild(0) : target.transform;
            
            icon.transform.SetParent(visualChild);
            icon.transform.localPosition = new Vector3(0, 2f, 0); // height above the object
            
        }
    }
}
