using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health;

    public int score;

    public UnityEvent onDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        //take damage from health and check if dead
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //add score for the player and die
        //FindAnyObjectByType<GameManager>().AddScore(score);

        Destroy(gameObject);
        onDeath.Invoke();
    }
}
