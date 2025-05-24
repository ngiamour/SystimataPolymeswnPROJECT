using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public string powerUpType; // "Magnet", "Shield", "DoubleScore"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Power Touch");
            PowerUps powerUps = other.GetComponent<PowerUps>();
            if (powerUps != null)
            {
                
                powerUps.ActivatePowerUp(powerUpType);
            }

            Destroy(gameObject);
            Destroy(gameObject.GetComponentInParent<MeshRenderer>());
        }
    }
}
