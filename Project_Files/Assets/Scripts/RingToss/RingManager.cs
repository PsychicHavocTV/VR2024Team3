using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] private IList<GameObject> gameRings = new List<GameObject>();
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject waitingPosition;
    [SerializeField] private int remainingRings;

    public void AddRingToList(GameObject ringToAdd)
    {
        gameRings.Add(ringToAdd);
        return;
    }

    /// <summary>
    /// Spawns new rings and handles changes made to the number of rings remaining before the level ends.
    /// </summary>
    public void SpawnNextRing()
    {
        remainingRings -= 1; // Decreases the remaining amount of rings by 1.
        if (remainingRings > 0)
        {
            GameObject newRing = Instantiate(ringPrefab); // Spawns a new ring at the waiting position.
            newRing.transform.position = new Vector3(waitingPosition.transform.position.x, waitingPosition.transform.position.y, waitingPosition.transform.position.z);
            RingDetector rD = newRing.GetComponentInChildren<RingDetector>();
            rD.waitingPosition = waitingPosition;
            newRing.gameObject.transform.SetParent(null); // Clears the rings parent object.
            newRing.gameObject.transform.rotation = new Quaternion(90, 0, 0, 90);
        }
        else
        {
            foreach (GameObject gameRing in gameRings)
            {
                GameObject tempObj = gameRing;
                gameRings.Remove(gameRing);
                Destroy(tempObj);
            }
        }
        RingCountCheck();
        return;
    }

    private void RingCountCheck()
    {
        if (remainingRings <= 0)
        {
            // !!Finish the Ring Toss Game.!!
            GameManager.singleton.FinishCurrentGame();
        }
    }
}
