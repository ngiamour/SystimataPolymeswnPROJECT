using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public Transform player;
    public Transform spawnPos;

    public float spawnRate = 1.5f;

    [SerializeField] private bool isTree = false;
    private PlayerMove _playerMove;
    private float _timer;
    
    [Header("Tree Stuff")]
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float repeatRate = 2;

    private WinManager _winManager;
    
    public GameObject iconBillboardPrefab;
    [SerializeField] private Sprite powerSprite;
    [SerializeField] private Sprite obstacleSprite;
    
    private void Start()
    {
        _winManager = FindObjectOfType<WinManager>();
        _playerMove = FindObjectOfType<PlayerMove>();
        if(isTree == true)
            InvokeRepeating("SpawnTree",startDelay,repeatRate);

        if (PlayerPrefs.GetInt("EasyMode", 0) == 1)
        {
            Debug.Log("Easy mode enabled from PlayerPrefs");
            spawnRate *= 2;
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnRate)
        {
            _timer = 0;
            if (_playerMove.gameOver == false && _winManager.gameWon==false && _winManager.stopSpawning==false)
            {
                if(isTree == false)
                    SpawnObject();
            }
        }
    }

    void SpawnObject()
    {
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
        SpawnableObject so = prefab.GetComponent<SpawnableObject>();
        if (so == null)
        {
            Debug.LogWarning("Prefab missing SpawnableObject script");
            return;
        }

        Vector3 spawnPosition = CalculateSpawnPosition(so.spawnType);
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        
        TryAttachIcon(spawnedObject);
        
        Vector3 CalculateSpawnPosition(SpawnType type)
        {
            float x = spawnPos.position.x;
            float z = player.position.z + 40f; // spawn ahead
            float y = 0.1f;

            switch (type)
            {
                case SpawnType.GroundOnly:
                    y = 0.1f; // ground level
                    break;
                case SpawnType.AirOnly:
                    x = spawnPos.position.x;
                    y = 2f;
                    break;
                case SpawnType.Flexible:
                    float[] yValues = {0.1f, 2f};
                   // y = Random.value > 0.5f ? 0.3f : Random.Range(0.1f, 4f)};
                    y = yValues[Random.Range(0, yValues.Length)];
                    break;
            }

            return new Vector3(x, y, z);
        }
    }
    
    void SpawnTree()
    {
        if (_playerMove.gameOver == false && _winManager.gameWon==false && _winManager.stopSpawning == false)
        {
            GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
            Vector3 spawnPosition = CalculateSpawnPosition();
            Instantiate(prefab, spawnPosition, prefab.transform.rotation);

            Vector3 CalculateSpawnPosition()
            {
                var position = spawnPos.position;
                float x = position.x;
                float z = position.z;
                float y = 0f;

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
            icon.transform.localPosition = new Vector3(0, 3f, 0); // height above the object
            
            // Find the icon's Image component
            RawImage iconImage = icon.GetComponentInChildren<RawImage>();

            if (iconImage != null)
            {
                SpawnableIcon so = target.GetComponent<SpawnableIcon>();
                if (so != null)
                {
                    switch (so.category)
                    {
                     //   case SpawnCategory.PowerUp:
                     //       iconImage.texture = powerSprite.texture;
                      //      break;
                        case SpawnCategory.Obstacle:
                            iconImage.texture = obstacleSprite.texture;
                            break;
                    }
                }
            }
            if (!target)
            {
                Destroy(iconImage);
            }
        }
    }
}
