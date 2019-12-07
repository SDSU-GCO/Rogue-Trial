using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    AudioMixerGroup audioMixerGroup;
    [SerializeField, Min(0), ValidateInput("inputNotEqualToZero", "Error: 'Fade Speed' can not be 0!")]
    float fadeSpeed=1.5f;
    [SerializeField]
    public AudioClip[] tracks;
    [SerializeField, ReadOnly]
    AudioClip nowPlaying;
    AudioSource current;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    bool inputNotEqualToZero(float fadeSpeed) => fadeSpeed!=0;

    // Start is called before the first frame update
    void Start()
    {
        Play();
        current.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(current.isPlaying == true && tracks.Contains(current.clip) != true)
        {
            Play();
            current.volume = 0;
        }
        else if(current.isPlaying!=true)
        {
            Play();
            current.volume = 1;
        }
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach(AudioSource aud in audioSources)
        {
            if(aud == current)
            {
                aud.volume = Mathf.Min(1, aud.volume + Time.unscaledDeltaTime / fadeSpeed);
            }
            else
            {
                aud.volume = Mathf.Max(0, aud.volume - Time.unscaledDeltaTime / fadeSpeed);
                if (aud.volume == 0)
                    aud.Stop();
            }
        }
    }

    AudioClip SelectRandomTrack() => tracks.Length == 0 ? null : tracks[Random.Range(0, tracks.Length)];

    private void Play(float volume = 1)
    {
        current = GetAudioSource();
        current.volume = volume;
        current.loop = false;
        current.spatialize = true;
        current.outputAudioMixerGroup = audioMixerGroup;
        current.clip = nowPlaying =SelectRandomTrack();
        if (current.clip != null)
            current.Play();
    }

    AudioSource GetAudioSource()
    {
        AudioSource rtnVal = null;

        bool keepLooking = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources.TakeWhile(x => { return keepLooking; }))
        {
            if (audioSource.isPlaying != true)
            {
                keepLooking = false;
                rtnVal = audioSource;
            }
        }

        rtnVal = rtnVal == null?gameObject.AddComponent<AudioSource>(): rtnVal;
        return rtnVal;
    }
}
