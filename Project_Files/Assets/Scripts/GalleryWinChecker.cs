using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryWinChecker : MonoBehaviour
{
    public List<GameObject> poppers;

    public void CheckPoppers()
    {
        /*
        bool allDead = true;

        foreach (GameObject go in poppers)
        {
            if (go != null)
            {
                allDead = false;
            }
        }*/

        if (poppers.Count == 1)
        {
            GameManager.singleton.FinishCurrentGame();
        }
    }
}
