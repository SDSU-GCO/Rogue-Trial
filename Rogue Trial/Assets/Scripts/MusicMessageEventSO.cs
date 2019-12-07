using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteSheep.Events;

[CreateAssetMenu(fileName = "new MusicMessageEventSO", menuName = "ScriptableObjects/MusicMessageEventSO")]
public class MusicMessageEventSO : ScriptableObject
{
    [HideInInspector]
    public QuickEvent Event = new QuickEvent();
    [SerializeField, HideInInspector]
    AudioClip[] audioClips = null;
    public AudioClip[] AudioClips
    {
        get
        {
            return audioClips;
        }
        set
        {
            audioClips = value;
            Event.Invoke();
        }
    }
}
