using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    public TMP_Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResults(List<float> times, string[] levelNames)
    {
        string results = "here are your results \n";

        for (int i = 0; i < levelNames.Length; i++) {
            results += levelNames[i] + " time: " + times[i].ToString("0.00") + "\n\n";
        }

        float totalTime = 0;

        for (int i = 0; times.Count > i; i++)
        {
            totalTime += times[i];
        }

        results += "\n your final times was: " + totalTime.ToString("0.00");

        resultText.text = results;
    }
}
