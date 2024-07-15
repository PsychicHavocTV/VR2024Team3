using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtTransform : MonoBehaviour
{
    public Transform spawn;

    public GameObject prefab;

    public float resetTimer;

    public void Spawn()
    {
        GameObject coin = Instantiate(prefab, spawn.position, Quaternion.identity);
        Destroy(coin, resetTimer);
        StartCoroutine(ReactivateButton());
    }

    public IEnumerator ReactivateButton()
    {
        yield return new WaitForSeconds(resetTimer);
        gameObject.SetActive(true);
    }
}
