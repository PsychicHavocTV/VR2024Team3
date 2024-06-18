using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryWinChecker : MonoBehaviour
{
    public GameObject[] poppers;

    public void CheckPoppers()
    {
        bool allDead = true;

        foreach(GameObject go in poppers)
        {
            if(go != null)
            {
                allDead = false;
            }
        }

        if (allDead == true)
        {
            GameManager.singleton.FinishCurrentGame();
        }
}
