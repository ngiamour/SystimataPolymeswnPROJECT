using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyIcon : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DestroyIcon()
    {
        transform.gameObject.SetActive(false);
    }
}
