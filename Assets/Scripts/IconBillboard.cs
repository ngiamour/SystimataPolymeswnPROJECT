using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBillboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
