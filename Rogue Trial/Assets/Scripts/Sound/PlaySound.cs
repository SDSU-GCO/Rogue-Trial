using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;

public class PlaySound : MonoBehaviour
{
    List<AudioSource> audioSources = new List<AudioSource>();

#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    AudioMixerGroup audioMixerGroup;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    public bool loop;
    public AudioClip defaultFile;
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneEventSO crossSceneEvent;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    private void Start()
    {
        if (crossSceneEvent != null)
            crossSceneEvent.Event.AddListener(playClip);
        else
            playClip();
    }
    private void OnDisable()
    {
        if (crossSceneEvent != null)
            crossSceneEvent.Event.RemoveListener(playClip);
    }
    public void playClip()
    {
        AudioSource audioSource = GetAudioSource();
        audioSource.clip = defaultFile;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.Play();
        return;
    }

    private void OnValidate()
    {
        if (audioMixerGroup == null)
            Debug.LogError(this);
    }


    AudioSource GetAudioSource()
    {
        AudioSource rtnVal=null;

        audioSources.Clear();
        audioSources.AddRange(GetComponents<AudioSource>());
        foreach(AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying != true)
                rtnVal = audioSource;
        }

        if (rtnVal == null)
            rtnVal = gameObject.AddComponent<AudioSource>();

        rtnVal.loop = loop;
        return rtnVal;
    }

    public void PlayFile(string filePath)
    {
        AudioSource audioSource = GetAudioSource(); 
        audioSource.clip = Resources.Load(filePath) as AudioClip;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.Play();
        return;
    }
}
