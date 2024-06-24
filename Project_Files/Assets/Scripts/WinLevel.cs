using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    //you can use this yo end a level anytime you want
    public void Win()
    {
        GameManager.singleton.FinishCurrentGame();
    }
}
