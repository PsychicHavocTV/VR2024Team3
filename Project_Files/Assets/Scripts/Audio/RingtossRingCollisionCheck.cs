using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingtossRingCollisionCheck : MonoBehaviour
{
    [SerializeField] private RingtossRingAudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponentInParent<RingtossRingAudioController>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ENTERED COLLISION");
        audioController.triggerSound();
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

}
