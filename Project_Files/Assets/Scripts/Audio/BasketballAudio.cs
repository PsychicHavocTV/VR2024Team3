using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballAudio : MonoBehaviour
{
    [SerializeField] private AudioSource bbSource;
    [SerializeField] private bool hasMultipleSounds;
    public AudioClip[] sounds;

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
        if (other.tag.ToLower() == "level" || other.tag.ToLower() == "floor")
        {
            if (hasMultipleSounds == true)
            {
                int soundChoice = 0;
                soundChoice = Random.Range(0, sounds.Length - 1);
                Debug.Log("Sound Choice: " + soundChoice);
                SoundManager.Instance.PlayFromSource(bbSource, sounds[soundChoice]);
            }
            else
            {
                SoundManager.Instance.PlayFromSource(bbSource, sounds[0]);
            }
        }
    }

}
