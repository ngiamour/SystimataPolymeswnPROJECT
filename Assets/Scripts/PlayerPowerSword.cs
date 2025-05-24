using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerSword : MonoBehaviour
{
    [SerializeField] private GameObject swordModel;

    public bool hasSword = false;

    [SerializeField] private PowerUpUIManager powerUI;

    [SerializeField] private float hasSwordTimer;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(other.GetComponentInParent<MeshRenderer>());
            
            audioSource.PlayOneShot(audioClip, 0.5f);
            swordModel.SetActive(true);
            StartCoroutine(ShowDurationTimer());
            
            //Debug.Log("Touched A Sword!");
            
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ShowDurationTimer()
    {
        hasSword = true;
        if(powerUI)
            powerUI.ShowUI(PowerUpType.Sword,hasSwordTimer);
        
        yield return new WaitForSeconds(hasSwordTimer);

        hasSword = false;
        powerUI.ShowUI(PowerUpType.Sword,hasSwordTimer);

        if (powerUI != null)
        {
            powerUI.HideUI(PowerUpType.Sword);
            swordModel.SetActive(false);
        }
    }
}
