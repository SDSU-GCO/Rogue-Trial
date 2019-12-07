using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField]
    MusicMessageEventSO musicMessageEventSO;
    [SerializeField]
    AudioClip[] audioClips;

#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value
    public virtual void OnValidate()
    {
        if (Application.isEditor)
        {
            if (musicMessageEventSO==null)
            {
#if UNITY_EDITOR
                musicMessageEventSO = AssetManagement.FindAssetByType<MusicMessageEventSO>();
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }
    }

    private void Start()
    {
        musicMessageEventSO.AudioClips = audioClips;
    }

}
