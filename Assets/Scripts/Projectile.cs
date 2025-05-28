using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obs"))
        {
           // Debug.Log("Bullet touch!");
           Transform parent = other.transform.parent;
           if (parent != null)
           {
               AutoDestroyIcon iconScript = parent.GetComponentInChildren<AutoDestroyIcon>();
               if (iconScript != null)
               {
                   iconScript.DestroyIcon();
               }
           }
           
           Destroy(other.gameObject); // or apply damage
           Destroy(gameObject);
        }
    }
}
