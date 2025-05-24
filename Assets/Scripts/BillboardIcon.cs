using UnityEngine;

public class BillboardIcon : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        gameObject.SetActive(AccessibilityManager.Instance != null && AccessibilityManager.Instance.iconsAboveObjects);
    }

    void LateUpdate()
    {
        if (!gameObject.activeSelf) return;
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }
}