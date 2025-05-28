using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    private PowerUps _powerUps;
    [SerializeField] private GameObject shieldObject;

    private void Start()
    {
        _powerUps = FindObjectOfType<PowerUps>();
    }

    void Update()
    {
        if (!_powerUps.hasShield)
        {
            shieldObject.SetActive(false);
            return;
        }
        shieldObject.SetActive(true);
        // Look for obs in a short range
        Collider[] nearbyHits = Physics.OverlapSphere(transform.position, 1.2f);
        foreach (var hit in nearbyHits)
        {
            
            if (hit.CompareTag("Obs"))
            {
                _powerUps.hasShield = false;

                if (_powerUps.shieldVFX)
                    _powerUps.shieldVFX.SetActive(false);

                if (_powerUps.powerUpUI)
                    _powerUps.powerUpUI.HideUI(PowerUpType.Shield);

                Debug.Log("Shield destroyed: " + hit.name);
                shieldObject.SetActive(false);
                Destroy(hit.gameObject);
                Transform parent = hit.transform.parent;
                if (parent != null)
                {
                    AutoDestroyIcon iconScript = parent.GetComponentInChildren<AutoDestroyIcon>();
                    if (iconScript != null)
                    {
                        iconScript.DestroyIcon();
                    }
                }
                

                break; // Use shield once only
            }
        }
    }
}
