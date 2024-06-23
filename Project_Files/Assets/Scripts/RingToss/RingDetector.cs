using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RingDetector : MonoBehaviour
{
    [SerializeField] private bool foundCatcher = false;
    [SerializeField] private bool onCatcher = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.ToLower() == "catcher")
        {
            if (foundCatcher == false)
            {
                foundCatcher = true;
            }
            Debug.Log("Found Catcher..");
        }
        if (other.gameObject.tag.ToLower() == "catcherbase")
        {
            if (onCatcher == false)
            {
                onCatcher = true;
            }
            Debug.Log("Ring is on the pole.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "catcher")
        {
            if (foundCatcher == true)
            {
                foundCatcher = false;
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        foundCatcher = false;
        onCatcher = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
