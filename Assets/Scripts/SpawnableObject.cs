using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    GroundOnly, AirOnly, Flexible
}

public class SpawnableObject : MonoBehaviour
{
    public SpawnType spawnType = SpawnType.GroundOnly;
}
