using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextCoin : MonoBehaviour
{
    private CoinManager _coinManager;
    [SerializeField] private TextMeshProUGUI coinsText;
    
    // Start is called before the first frame update
    void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
        coinsText.text = _coinManager.totalCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
