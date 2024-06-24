using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTrigger : MonoBehaviour
{

    public string tagToCheck;

    public UnityEvent OnHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tagToCheck != "")
        {
            if(other.tag == tagToCheck)
            {
                OnHit.Invoke();
            }
        } else
        {
            OnHit.Invoke();
        }
    }
}
