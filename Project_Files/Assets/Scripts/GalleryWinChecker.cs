using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GalleryWinChecker : MonoBehaviour
{
    public List<GameObject> poppers;

    public TMP_Text popperCounter;

    public string beforeNumber;

    public void CheckPoppers()
    {

        if (poppers.Count == 1)
        {
            GameManager.singleton.FinishCurrentGame();
        }
    }

    private void Update()
    {
        if(popperCounter)
        {
            popperCounter.text = beforeNumber + popperCounter;
        }
    }


}
