using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private CoinManager _coinManager;

    private Coin scoreGetter;
    // Start is called before the first frame update
    void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Coin"))
        {
        	_coinManager.PickedUp(other.gameObject.GetComponent<Coin>().scoreToAdd);
        	Debug.Log("Touched a coin");
            Destroy(other.gameObject);
        }
    }
}
