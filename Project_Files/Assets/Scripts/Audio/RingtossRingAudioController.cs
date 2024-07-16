using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingtossRingAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private bool multipleSFX = false;
    private int soundSelection;
    public AudioClip[] sounds;

    public void triggerSound()
    {
        if (soundSource.isPlaying == false)
        {
            if (multipleSFX == true)
            {
                soundSelection = Random.Range(0, sounds.Length - 1);
                Debug.Log("Sound Selection: " + soundSelection);
            }
            else
            {
                soundSelection = 0;
            }
            SoundManager.Instance.PlayFromSource(soundSource, sounds[soundSelection]);
        }
        return;
    }
}
