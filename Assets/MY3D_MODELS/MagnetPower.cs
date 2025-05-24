using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    private PowerUps _powerUps;

    private void Start()
    {
        _powerUps = FindObjectOfType<PowerUps>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_powerUps.hasMagnet)
        {
            Collider[] nearbyCoins = Physics.OverlapSphere(transform.position, 5f);
            foreach (var col in nearbyCoins)
            {
                if (col.CompareTag("Coin"))
                {
                    col.transform.position = Vector3.MoveTowards(col.transform.position, transform.position, 10f * Time.deltaTime);
                }
            }
        }
    }
}
