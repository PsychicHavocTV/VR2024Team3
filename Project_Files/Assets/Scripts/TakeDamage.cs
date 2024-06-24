using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    float damage;

    //call take damage on the game manager
    public void MinusTimer()
    {
        GameManager.singleton.TakeDamage(damage);
    }
}
