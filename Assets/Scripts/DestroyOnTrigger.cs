using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{

    private Transform player;
    private float destroyDist = 15.0f;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        if (player == null) return;

        if (transform.position.z < player.position.z - destroyDist)
        {
            Destroy(gameObject);
        }
    }
}
