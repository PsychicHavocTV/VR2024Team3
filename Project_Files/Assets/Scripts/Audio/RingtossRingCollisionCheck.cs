using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingtossRingCollisionCheck : MonoBehaviour
{
    private RingtossRingAudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponentInParent<RingtossRingAudioController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioController.triggerSound();
    }

}
