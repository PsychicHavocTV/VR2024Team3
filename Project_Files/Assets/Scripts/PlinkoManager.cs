using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoManager : MonoBehaviour
{
    public List<PlinkoWinBox> wonBoxes;

    public int scoreToWin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wonBoxes.Count == scoreToWin)
        {
            GameManager.singleton.FinishCurrentGame();
        }
    }
}
