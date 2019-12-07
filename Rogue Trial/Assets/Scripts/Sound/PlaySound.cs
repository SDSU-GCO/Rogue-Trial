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
    public bool randomizePitch=false;
    public bool randomizeClipPosition=false;
    public bool PlayOnEnable=false;
    public float Volume=1;
    [BoxGroup("Spatialize")]
    public bool SpatializeIn3D;
    [SerializeField, ShowIf("SpatializeIn3D"), BoxGroup("Spatialize"), Range(0, 5)]
    float dopplerLevel=1;
    [SerializeField, ShowIf("SpatializeIn3D"), BoxGroup("Spatialize"), Range(0, 360)]
    float spread;
    [SerializeField, ShowIf("SpatializeIn3D"), BoxGroup("Spatialize")]
    AudioRolloffMode VolumeRolloff;
    [SerializeField, ShowIf("SpatializeIn3D"), BoxGroup("Spatialize"), Min(0)]
    float minDistance=0;
    [SerializeField, ShowIf("SpatializeIn3D"), BoxGroup("Spatialize"), Min(0)]
    float maxDistance=500;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    public AudioClip defaultFile;

    bool ShowDelayField() => loop && UseDelay && !UseRandomDelay;
    bool ShowUseRandomDelayField() => loop && UseDelay;
    bool ShowDelayRangeField() => loop && UseDelay && UseRandomDelay;

#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    CrossSceneEventSO crossSceneEvent;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    private void OnDisable()
    {
        if (crossSceneEvent != null)
            crossSceneEvent.Event.RemoveListener(playClip);
    }
    private void OnEnable()
    {
        if (crossSceneEvent != null)
            crossSceneEvent.Event.AddListener(playClip);
        if (PlayOnEnable)
            playClip();
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
            setupAudioSourceAndPlay(ref audioSource);
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
        setupAudioSourceAndPlay(ref audioSource);
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
        rtnVal.playOnAwake = false;
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
                setupAudioSourceAndPlay(ref audioSource);
            }
            yield return null;
        }
    }

    void setupAudioSourceAndPlay(ref AudioSource audioSource)
    {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.pitch = randomizePitch == true ? Random.Range(0.85f, 1.15f) : 1;
        audioSource.volume = Volume;

        if(SpatializeIn3D)
        {
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = VolumeRolloff;
            audioSource.dopplerLevel = dopplerLevel;
            audioSource.spread = spread;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
        }
        else
        {
            audioSource.spatialBlend = 0;
        }

        if (randomizeClipPosition)
        {
            audioSource.time = Random.Range(0, audioSource.clip.length);
        }

        audioSource.Play();
    }
}
