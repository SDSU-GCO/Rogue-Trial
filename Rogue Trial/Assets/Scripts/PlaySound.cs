using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;

public class PlaySound : MonoBehaviour
{
    List<AudioSource> audioSources = new List<AudioSource>();

    [SerializeField, Required]
    AudioMixerGroup audioMixerGroup;

    public bool loop;
    public AudioClip defaultFile;
    [SerializeField]
    CrossSceneEvent crossSceneEvent;

    private void Start()
    {
        crossSceneEvent.SomeEvent.AddListener(playClip);
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
