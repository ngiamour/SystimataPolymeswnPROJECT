using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType { Magnet, Shield, DoubleScore,Sword }

public class PowerUpUIManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpUIEntry
    {
        public PowerUpType type;
        public GameObject uiObject;
        public TextMeshProUGUI timerText;

        [HideInInspector] public float timer;
        [HideInInspector] public float duration;
        [HideInInspector] public bool isActive;
    }

    public List<PowerUpUIEntry> powerUpEntries;

    private Dictionary<PowerUpType, PowerUpUIEntry> uiLookup;

    void Awake()
    {
        uiLookup = new Dictionary<PowerUpType, PowerUpUIEntry>();
        foreach (var entry in powerUpEntries)
        {
            entry.uiObject.SetActive(false);
            uiLookup[entry.type] = entry;
        }
    }

    void Update()
    {
        foreach (var entry in uiLookup.Values)
        {
            if (!entry.isActive) continue;

            entry.timer -= Time.deltaTime;
            entry.timerText.text = Mathf.Ceil(entry.timer).ToString();

            if (entry.timer <= 0)
                HideUI(entry.type);
        }
    }

    public void ShowUI(PowerUpType type, float duration)
    {
        if (!uiLookup.ContainsKey(type)) return;

        var ui = uiLookup[type];
        ui.duration = duration;
        ui.timer = duration;
        ui.timerText.text = Mathf.Ceil(duration).ToString();
        ui.uiObject.SetActive(true);
        ui.isActive = true;
    }

    public void HideUI(PowerUpType type)
    {
        if (!uiLookup.ContainsKey(type)) return;

        var ui = uiLookup[type];
        ui.isActive = false;
        ui.uiObject.SetActive(false);
    }
}