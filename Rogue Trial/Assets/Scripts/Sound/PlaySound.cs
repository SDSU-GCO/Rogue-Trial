using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;
using System.Linq;


public class PlaySound : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    AudioMixerGroup audioMixerGroup;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    [BoxGroup("Loops")]
    public bool loop;
    [ShowIf("loop"), BoxGroup("Loops")]
    public bool UseDelay;
    [ShowIf("ShowUseRandomDelayField"), BoxGroup("Loops")]
    public bool UseRandomDelay;
    [ShowIf("ShowDelayField"), BoxGroup("Loops")]
    public float Delay;
    [ShowIf("ShowDelayRangeField"), BoxGroup("Loops")]
    public float MaxDelay;
    [ShowIf("ShowDelayRangeField"), BoxGroup("Loops")]
    public float MinDelay;
    public bool randomizePitch;
    public AudioClip defaultFile;

    bool ShowDelayField() => loop && UseDelay && !UseRandomDelay;
    bool ShowUseRandomDelayField() => loop && UseDelay;
    bool ShowDelayRangeField() => loop && UseDelay && UseRandomDelay;

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
        if(loop && UseDelay)
        {
            StartCoroutine(StartLooping());
        }
        else
        {
            AudioSource audioSource = GetAudioSource();
            audioSource.clip = defaultFile;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.pitch = randomizePitch == true ? Random.Range(0.85f, 1.15f) : 1;
            audioSource.Play();
        }
        return;
    }

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (audioMixerGroup == null)
                Debug.LogError(this);
        }
    }

    public void PlayFile(string filePath)
    {
        AudioSource audioSource = GetAudioSource();
        audioSource.clip = Resources.Load(filePath) as AudioClip;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.pitch = randomizePitch == true ? Random.Range(0.85f, 1.15f) : 1;
        audioSource.Play();
        return;
    }

    AudioSource GetAudioSource()
    {
        AudioSource rtnVal=null;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        bool keepLooking = true;
        foreach(AudioSource audioSource in audioSources.TakeWhile(x=> { return keepLooking; }))
        {
            if (audioSource.isPlaying != true)
            {
                keepLooking = false;
                rtnVal = audioSource;
            }
        }

        if (rtnVal == null)
            rtnVal = gameObject.AddComponent<AudioSource>();

        rtnVal.loop = loop;
        return rtnVal;
    }

    IEnumerator StartLooping()
    {
        float currentTimer = UseRandomDelay ? Random.Range(MinDelay, MaxDelay) : Delay;

        while (loop)
        {
            currentTimer = Mathf.Max(0f, currentTimer - Time.deltaTime);
            if(currentTimer<=0)
            {
                currentTimer = UseRandomDelay ? Random.Range(MinDelay, MaxDelay) : Delay;
                AudioSource audioSource = GetAudioSource();
                audioSource.clip = defaultFile;
                audioSource.outputAudioMixerGroup = audioMixerGroup;
                audioSource.pitch = randomizePitch == true ? Random.Range(0.85f, 1.15f) : 1;
                audioSource.Play();
            }
            yield return null;
        }
    }
}
