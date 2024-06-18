using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("how long the player has to live")]
    public float lifeTimer;

    [Tooltip("how long the player has lived. this will be shown to the player at the end if the game")]
    public float scoreTimer;

    [Tooltip("whether or not the game has started")]
    private bool gameStarted = false;

    [Tooltip("where the circler Enemies will spawn")]
    public Transform[] circleSpawners;

    [Tooltip("where the popper Enemies will spawn")]
    public Transform[] popperSpawners;

    [Tooltip("the circler enemy prefab")]
    public GameObject circler;

    [Tooltip("the popper enemy prefab")]
    public GameObject popper;

    [Tooltip("minimum time until the next circler fish appears")]
    public float minEnemyTime;

    [Tooltip("minimum time until the next circler fish appears")]
    public float maxEnemyTimer;

    [Tooltip("the maximum amount of circler fish that can spawn at once")]
    private float enemy = 1;

    [Tooltip("how many fish must be killed before min and max timer decrease")]
    public int amountToKillToDecreaseTimer;

    [Tooltip("how many enemies have been killed since the timer last decreased")]
    private int deadEnemyCounter;

    [Tooltip("what the timer will be decreased by when the enemy kill goal has been reached")]
    public float divideTimerBy;

    [Tooltip("when a circler is killed the chance for than one is increased by this amount")]
    public float amountTheChanceForEnemyIncreases;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            lifeTimer -= Time.deltaTime;

            scoreTimer += Time.deltaTime;
        }

        if(lifeTimer <= 0)
        {
            ///end game here
        }
    }

    public void StartSpawnCircler()
    {
        StartCoroutine(SpawnCircler());
    }

    public IEnumerator SpawnCircler()
    {
        yield return new WaitForSeconds(Random.Range(minEnemyTime, maxEnemyTimer));

        //we use circle incrementor so that when it passes 2 it now becomes possible for 2 at a time to spawn
        for (int i = 0; i <= Random.Range(1, (int)enemy); i++)
        {
            Instantiate(circler, circleSpawners[Random.Range(0, circleSpawners.Length)]);
        }

        enemy += amountTheChanceForEnemyIncreases;

        StartSpawnCircler();
    }

    public void StartSpawnPopper()
    {
        StartCoroutine(SpawnPopper());
    }

    public IEnumerator SpawnPopper()
    {
        yield return new WaitForSeconds(Random.Range(minEnemyTime, maxEnemyTimer));

        //we use circle incrementor so that when it passes 2 it now becomes possible for 2 at a time to spawn
        for (int i = 0; i <= Random.Range(1, (int)enemy); i++)
        {
            Instantiate(popper, popperSpawners[Random.Range(0, popperSpawners.Length)]);
        }

        enemy += amountTheChanceForEnemyIncreases;

        StartSpawnPopper();
    }

    public void StartGame()
    {
        gameStarted = true;
        StartSpawnCircler();
        StartSpawnPopper();
    }

    public void AddScore(float score)
    {
        lifeTimer += score;

        deadEnemyCounter++;

        if(deadEnemyCounter == amountToKillToDecreaseTimer)
        {
            deadEnemyCounter = 0;
            minEnemyTime /= divideTimerBy;
            maxEnemyTimer /= divideTimerBy;
        }
    }

    public void TakeDamage(float damage)
    {
        lifeTimer -= damage;
    }
}
