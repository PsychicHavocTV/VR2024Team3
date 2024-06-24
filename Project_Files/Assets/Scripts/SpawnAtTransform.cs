using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtTransform : MonoBehaviour
{
    public Transform spawn;

    public GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, spawn.position, Quaternion.identity);
    }
}
