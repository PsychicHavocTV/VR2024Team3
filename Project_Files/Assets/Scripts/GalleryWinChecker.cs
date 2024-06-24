using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryWinChecker : MonoBehaviour
{
    public List<GameObject> poppers;

    public void CheckPoppers()
    {

        if (poppers.Count == 0)
        {
            GameManager.singleton.FinishCurrentGame();
        }
    }
}
