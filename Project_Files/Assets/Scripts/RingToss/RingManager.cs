using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject waitingPosition;
    [SerializeField] private int remainingRings;

    /// <summary>
    /// Spawns new rings and handles changes made to the number of rings remaining before the level ends.
    /// </summary>
    public void SpawnNextRing()
    {
        remainingRings -= 1; // Decreases the remaining amount of rings by 1.
        GameObject newRing = Instantiate(ringPrefab, waitingPosition.transform); // Spawns a new ring at the waiting position.
        ringPrefab.gameObject.transform.SetParent(null); // Clears the rings parent object.
        RingCountCheck();
        return;
    }

    private void RingCountCheck()
    {
        if (remainingRings <= 0)
        {
            // !!Finish the Ring Toss Game.!!
            //GameManager.singleton.FinishCurrentGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
