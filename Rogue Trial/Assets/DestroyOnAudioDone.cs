using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class DestroyOnAudioDone : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    AudioMixerGroup audioMixerGroup;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetAudioSource();
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }

    }
    AudioSource GetAudioSource()
    {
        AudioSource rtnVal = null;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        bool keepLooking = true;
        foreach (AudioSource audioSource in audioSources.TakeWhile(x => { return keepLooking; }))
        {
            if (audioSource.isPlaying != true)
            {
                keepLooking = false;
                rtnVal = audioSource;
            }
        }

        if (rtnVal == null)
            rtnVal = gameObject.AddComponent<AudioSource>();
        return rtnVal;
    }
}
