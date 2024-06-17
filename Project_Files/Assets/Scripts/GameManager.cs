using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float lifeTimer;

    public float scoreTimer;

    private bool gameStarted = false;

    public Transform[] circleSpawners;

    public GameObject circlerFish;

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
        yield return new WaitForSeconds(7);

        Instantiate(circlerFish, circleSpawners[Random.Range(0, circleSpawners.Length)]);
    }

    public void StartGame()
    {
        gameStarted = true;
        StartSpawnCircler();
    }
}
