using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class DestroyOnAudioDone : MonoBehaviour
{
    [SerializeField]
    AudioClip audioClip;
    AudioMixerGroup audioMixerGroup;

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
