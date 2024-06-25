using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoManager : MonoBehaviour
{
    private List<PlinkoWinBox> wonBoxes;

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

    public void AddBox(PlinkoWinBox box)
    {
        wonBoxes.Add(box);
    }

    public void RemoveBox(PlinkoWinBox box)
    {
        wonBoxes.Remove(box);
    }
}
