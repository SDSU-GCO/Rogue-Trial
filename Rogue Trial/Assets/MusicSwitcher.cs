using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField]
    MusicMessageEventSO musicMessageEventSO;
    [SerializeField]
    AudioClip[] audioClips;

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
