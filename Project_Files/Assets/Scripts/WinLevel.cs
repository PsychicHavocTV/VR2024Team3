using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public void Win()
    {
        GameManager.singleton.FinishCurrentGame();
    }
}
