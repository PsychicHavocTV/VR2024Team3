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

    [Tooltip("the circler enemy prefab")]
    public GameObject circler;

    [Tooltip("minimum time until the next circler fish appears")]
    public float minCirclerTime;

    [Tooltip("minimum time until the next circler fish appears")]
    public float maxCirclerTimer;

    [Tooltip("the maximum amount of circler fish that can spawn at once")]
    private float circlerIncrementor = 1;

    [Tooltip("how many fish must be killed before min and max timer decrease")]
    public int amountToKillToDecreaseTimer;

    [Tooltip("how many enemies have been killed since the timer last decreased")]
    private int deadEnemyCounter;

    [Tooltip("what the timer will be decreased by when the enemy kill goal has been reached")]
    public float divideTimerBy;

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
        yield return new WaitForSeconds(Random.Range(minCirclerTime, maxCirclerTimer));

        //we use circle incrementor so that when it passes 2 it now becomes possible for 2 at a time to spawn
        for (int i = 0; i <= Random.Range(1, (int)circlerIncrementor); i++)
        {
            Instantiate(circler, circleSpawners[Random.Range(0, circleSpawners.Length)]);
        }

        circlerIncrementor += .035f;

        StartSpawnCircler();
    }

    public void StartGame()
    {
        gameStarted = true;
        StartSpawnCircler();
    }

    public void AddScore(float score)
    {
        lifeTimer += score;

        deadEnemyCounter++;

        if(deadEnemyCounter == amountToKillToDecreaseTimer)
        {
            deadEnemyCounter = 0;
            minCirclerTime /= divideTimerBy;
            maxCirclerTimer /= divideTimerBy;
        }
    }
}
