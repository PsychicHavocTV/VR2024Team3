using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public int hits = 0;
    public int goal;
    public void CheckWin()
    {
        hits++;
        if(hits >= goal)
        {
            Win();
        }
    }

    public void IncreaseHitsBy(int hitIncreaser)
    {
        hits += hitIncreaser;
        if (hits >= goal)
        {
            Win();
        }
    }

    //you can use this your end a level anytime you want
    public void Win()
    {
        GameManager.singleton.FinishCurrentGame();
    }
}
