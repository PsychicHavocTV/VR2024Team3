using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTrigger : MonoBehaviour
{

    public string tagToCheck;

    public UnityEvent OnHit;

    //should be able to use this on multiple triggers for defferant circumstances
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
